﻿using Domain.Enums;

namespace HR_System.Entities;

public class User: BaseEntity
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }

    public UserRole Role { get; set; }

    public Employee? EmployeeProfile { get; set; }
}