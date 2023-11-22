using Common.Models;
using Lab1_Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab1_Web.Controllers;

public class UserController : Controller
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    public ActionResult Index()
    {
        return View("UserPage");
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _service.GetAllUsers();
        return View("AllUsers", users);
    }

    [HttpGet]
    public async Task<IActionResult> CreateUser()
    {
        return View("CreateUser");
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserCreateModel user)
    {
        await _service.CreateUser(user);

        return RedirectToAction("GetAllUsers");
    }
}