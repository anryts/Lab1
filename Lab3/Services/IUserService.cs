using Common.Models;
using Lab1_Web.Entities;
using Lab3.Entities;

namespace Lab3.Services;

public interface IUserService
{
    Task<Guid> CreateUser(UserCreateModel model);
    Task DeleteUser(UserDeleteModel model);
    Task UpdateUser(UserUpdateModel model);
    Task<List<User>> GetAllUsers(int page, int pageSize, string? sortBy, bool? isAsc);
}
