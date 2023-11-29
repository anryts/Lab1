using Common.Models;
using Dapper;
using Lab1_Data;
using Lab1_Data.Entities;
using Lab1_Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1_Web.Services;

internal sealed class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public UserService(AppDbContext appDbContext, ISqlConnectionFactory sqlConnectionFactory)
    {
        _context = appDbContext;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Guid> CreateUser(UserCreateModel model)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Email = model.Email,
            GenderId = model.GenderId
        };
        await using var connection = _sqlConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        var command = new CommandDefinition(
            "INSERT INTO Users (Id, Name, Email, GenderId) " +
            "VALUES (@Id, @Name, @Email, @GenderId)",
            user);
        await connection.ExecuteAsync(command);

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

    public async Task<List<User>> GetAllUsers(int page, int pageSize, string? sortBy, bool? isAsc)
    {
        var users = await _context.Users
            .Skip((page - 1) * pageSize)
            .Take(page)
            .ToListAsync();

        if (sortBy != null)
        {
            users = isAsc == true
                ? users.OrderBy(user => user.GetType().GetProperty(sortBy)?.GetValue(user, null)).ToList()
                : users.OrderByDescending(user => user.GetType().GetProperty(sortBy)?.GetValue(user, null)).ToList();
        }

        return users;
    }

    public async Task UpdateUser(UserUpdateModel model)
    {
        var user = await _context.Users
                       .FirstOrDefaultAsync(user => user.Id == model.Id)
                   ?? throw new Exception("User not found");

        user.Name = model.Name ?? user.Name;
        user.Email = model.Email ?? user.Email;
        user.GenderId = model.GenderId ?? user.GenderId;

        _context.Users.Update(user);
    }
}