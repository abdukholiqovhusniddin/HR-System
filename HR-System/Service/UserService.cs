using System.Text;
using System.Xml;
using HR_System.DTOs;
using HR_System.Entities;
using HR_System.Exceptions;
using HR_System.Helpers;
using HR_System.Interfaces.Repository;
using HR_System.Interfaces.Service;
using HR_System.JwtAuth;
using Mapster;
using Newtonsoft.Json;
using static HR_System.DTOs.UserAuthDto;
using Formatting = Newtonsoft.Json.Formatting;

namespace HR_System.Service;
public class UserService(IUserRepository userRepository, JwtService jwtService, 
    IUnitOfWork unitOfWork,
    IEmployerRepository employerRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmployerRepository _employerRepository = employerRepository;

    private readonly string telegramBotToken = "8230471687:AAHCx7Sofbo6VYk3_wTmHmSjbYJdKWn03Qw";

    public string GeneratePasswordForUser() =>
        PasswordHelper.PasswordGeneration();
    

    public async Task<UserDto> CreateUserAsync(UserRegisterDto userRegisterDto)
    {
        if (await _userRepository.ExistsAsync(userRegisterDto.Username))
            throw new ApiException("User or already exists.");
        if (await _userRepository.ExistsAsync(userRegisterDto.Email))
            throw new ApiException("Email or already exists.");
        if (!Enum.IsDefined(userRegisterDto.Role))
            throw new ApiException("Invalid role value.");

        string password = GeneratePasswordForUser();

        var userId = await _userRepository.CreateAsync(new User
        {
            Username = userRegisterDto.Username,
            Email = userRegisterDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = userRegisterDto.Role
        });

        UserDto? employer = await _employerRepository.CreateAsync(userId, userRegisterDto)
            ?? throw new ApiException("Employer creation failed.");

        employer.Password = password;
        employer.Username = userRegisterDto.Username;
        employer.Email = userRegisterDto.Email;
        employer.Role = userRegisterDto.Role;

        await _unitOfWork.SaveChangesAsync(cancellationToken: CancellationToken.None);
        _ = Task.Run(() => SendPasswordToTelegramWhenReady(userRegisterDto.Telegram, password));
        return employer;
    }

    private static long lastUpdateId = 0;
    private async Task SendPasswordToTelegramWhenReady(string username, string password)
    {
        Console.WriteLine($"⏳ Telegram: @{username} botga yozishini kutyapmiz...");

        while (true)
        {
            string apiUrl = $"https://api.telegram.org/bot{telegramBotToken}/getUpdates?offset={lastUpdateId + 1}";
            using var client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();

            using var doc = System.Text.Json.JsonDocument.Parse(json);
            var root = doc.RootElement;
            bool found = false;

            foreach (var result in root.GetProperty("result").EnumerateArray())
            {
                lastUpdateId = result.GetProperty("update_id").GetInt64();

                if (!result.TryGetProperty("message", out var message)) continue;
                var chat = message.GetProperty("chat");
                if (!chat.TryGetProperty("username", out var unameProp)) continue;
                string msgUsername = unameProp.GetString();
                long chatId = chat.GetProperty("id").GetInt64();

                if (string.Equals(msgUsername, username, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"✅ Telegram: @{username} botga yozdi! Parol yuborilmoqda...");
                    await SendPasswordViaTelegram(telegramBotToken, chatId.ToString(), password);
                    Console.WriteLine($"✅ Telegram: Parol @{username} ga yuborildi!");
                    found = true;
                    break;
                }
            }

            if (found) break;
            await Task.Delay(3000); // 3 sekund kutadi va yana tekshiradi
        }
    }

    private async Task SendPasswordViaTelegram(string botToken, string chatId, string password)
    {
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(30);

        string telegramApiUrl = $"https://api.telegram.org/bot{botToken}/sendMessage";
        string messageContent = $@"
🔐 <b>Sizning yangi parolingiz</b>

┌─────────────────────────
│ 🆔 <b>Parol:</b> <code>{password}</code>
└─────────────────────────

📋 <b>Qo'shimcha ma'lumot:</b>
• Parolni nusxalash uchun ustiga bosing
• Xavfsizlik uchun parolni o'zgartirishingizni tavsiya etamiz
• Bu parolni hech kimga bermang

⚠️ <b>Xavfsizlik eslatmasi:</b>
Bu parolni <u>maxfiy</u> saqlang va boshqalar bilan baham ko'rmang.

🤖 <i>Bu xabar avtomatik yuborilgan</i>
📅 <i>Yuborilgan vaqt: {DateTime.Now:dd.MM.yyyy HH:mm:ss}</i>
        ".Trim();

        var requestData = new
        {
            chat_id = chatId,
            text = messageContent,
            parse_mode = "HTML",
            disable_web_page_preview = true
        };

        string jsonData = JsonConvert.SerializeObject(requestData, Formatting.Indented);
        var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var httpResponse = await httpClient.PostAsync(telegramApiUrl, httpContent);
        // Xatoliklarni log qilish uchun javobni o'qish mumkin
        string responseText = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            Console.WriteLine($"❌ Telegram API xatolik: {responseText}");
        }
    }


    public async Task<string?> LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await _userRepository.GetByUsernameAsync(userLoginDto.Username);
        if (user == null)
            return null;
        if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            return null;
        return jwtService.GenerateToken(user);
    }

    public async Task<UserProfileDto?> GetByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ApiException("Username cannot be null.");

        var user = await _userRepository.GetByUsernameAsync(username, includeEmployeeProfile: true);

        if (user is null)
            throw new ApiException("User not found.");


        return user.Adapt<UserProfileDto>();
    }

    public async Task<UserProfileDto?> AssignRoleAsync(AssignRoleDto dto)
    {
        if (!Enum.IsDefined(typeof(UserRole), dto.Role))
            throw new Exception("Invalid role.");

        var user = await _userRepository.GetByUsernameAsync(dto.Username, includeEmployeeProfile: true);

        if (user == null)
            return null;

        user.Role = dto.Role;

        await _userRepository.UpdateAsync(user);

        return user.Adapt<UserProfileDto>();
    }
}
