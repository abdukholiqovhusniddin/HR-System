﻿namespace HR_System.JwtAuth;
public class AuthSettings
{
    public TimeSpan Expres { get; set; }
    public string? SecretKey { get; set; }
}