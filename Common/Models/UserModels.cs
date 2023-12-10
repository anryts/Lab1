using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1_Data.Entities;

namespace Common.Models;

public class UserCreateModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    public Guid? GenderId { get; set; }
}

public record UserUpdateModel (Guid Id, string? Name, string? Email, Guid? GenderId);
public record UserDeleteModel(Guid Id);
public record GetUsersModel(List<User> Users, int PageSize, int Page);



