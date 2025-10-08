namespace Application.Commons;
public static class PasswordHelper
{
    public static string PasswordGeneration()
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        Random random = new();
        char[] chars = new char[6];

        for (int i = 0; i < 6; i++)
        {
            chars[i] = validChars[random.Next(validChars.Length)];
        }

        return new string(chars);
    }
}