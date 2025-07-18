﻿using HR_System.Data;
using HR_System.Entities;
using HR_System.Exceptions;
using HR_System.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Repository;
public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;
    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string? username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ApiException("Username cannot be null or empty.");
        return await _context.Users.AnyAsync(n => n.Username == username);
    }

    public async Task<User> GetByUsernameAsync(string? username) =>
        await _context.Users.FirstOrDefaultAsync(n => n.Username == username)
            ?? throw new ApiException("User not found.");
}
