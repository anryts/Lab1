using Common.Models;
using Lab1_Data;
using Lab1_Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1_Web.Services;

internal sealed class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public async Task<Guid> CreateUser(UserCreateModel model)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Email = model.Email
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task DeleteUser(UserDeleteModel model)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Id == model.Id) 
            ?? throw new Exception("User not found");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task UpdateUser(UserUpdateModel model)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Id == model.Id)
            ?? throw new Exception("User not found");

        user.Name = model.Name ?? user.Name;
        user.Email = model.Email ?? user.Email;

        _context.Users.Update(user);
    }
}
