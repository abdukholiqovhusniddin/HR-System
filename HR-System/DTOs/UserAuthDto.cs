namespace HR_System.DTOs;
public class UserAuthDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
    }

    public class LoginRequestDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class LoginResponseDto
    {
        public string? Token { get; set; }
        public UserDto? User { get; set; }
    }
}
