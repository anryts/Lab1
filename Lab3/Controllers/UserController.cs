using Common.Models;
using Lab3.Data;
using Lab3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly Lab3Context _context;
    private readonly IUserService _service;

    public UserController(IUserService service, Lab3Context context)
    {
        _service = service;
        _context = context;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
        return View("UserPage");
    }

    [AllowAnonymous]
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers(int page = 1, int pageSize = 10, string? sortBy = null, bool isAsc = false)
    {
        //TODO: add normal pagination and sorting by enum prob???
        var users = await _service.GetAllUsers(page, pageSize, sortBy, isAsc);

        return View("AllUsers", users);
    }

    [HttpGet]
    public async Task<IActionResult> CreateUser()
    {
        var gendersIds = await _context.Genders.ToListAsync();
        ViewBag.Genders = new SelectList(gendersIds, "Id", "Name");
        return View("CreateUser");
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserCreateModel user)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateUser(user);

            return RedirectToAction("GetAllUsers");
        }

        return View("CreateUser", user);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateUser()
    {
        var gendersIds = await _context.Genders.ToListAsync();
        ViewBag.Genders = new SelectList(gendersIds, "Id", "Name");
        return View("UpdateUser");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UserUpdateModel user)
    {
        await _service.UpdateUser(user);

        return RedirectToAction("GetAllUsers");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteUser()
    {
        var users = await _context.Users.ToListAsync();
        ViewBag.Users = new SelectList(users, "Id", "Name");
        return View("DeleteUser");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(UserDeleteModel user)
    {
        await _service.DeleteUser(user);

        return RedirectToAction("GetAllUsers");
    }
}