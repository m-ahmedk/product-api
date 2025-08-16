﻿using Microsoft.EntityFrameworkCore;
using ProductDemo.Data;
using ProductDemo.Models;
using ProductDemo.Repositories.Interfaces;

namespace ProductDemo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<AppUser> AddAsync(AppUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
