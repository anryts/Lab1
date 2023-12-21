using Common.Models;
using Lab1_Data.Entities;
using Lab1_Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab1_Web.Controllers;

public static class Lab4Controller
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("users");

        group.MapGet("get_all_users", GetAllUsers).WithName(nameof(GetAllUsers));
        group.MapPost("create_user", CreateUser).WithName(nameof(CreateUser));
        group.MapPut("update_user", UpdateUser).WithName(nameof(UpdateUser));
        group.MapDelete("delete_user", DeleteUser).WithName(nameof(DeleteUser));
    }

    public static async Task<List<User>> GetAllUsers(IUserService service, [FromQuery]int page = 1, [FromQuery]int pageSize = 10,
        [FromQuery]string? sortBy = null,
        [FromQuery]bool isAsc = false)
    {
        //TODO: add normal pagination and sorting by enum prob???
        var users = await service.GetAllUsers(page, pageSize, sortBy, isAsc);

        return users;
    }

    public static async Task<IActionResult> CreateUser(IUserService service, [FromBody] UserCreateModel user)
    {
        await service.CreateUser(user);

        return new OkObjectResult("User created successfully");
    }


    public static async Task<IActionResult> UpdateUser(IUserService service, [FromBody]UserUpdateModel user)
    {
        await service.UpdateUser(user);

        return new OkObjectResult("User updated successfully");
    }

    public static async Task<IActionResult> DeleteUser(IUserService service, [FromQuery] Guid id)
    {
        await service.DeleteUser(id);

        return new OkObjectResult("User deleted successfully");
    }
}