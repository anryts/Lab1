using System.ComponentModel.DataAnnotations;
using Lab3.Entities;

namespace Lab1_Web.Entities;

public class Gender
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(10)]
    public string Name { get; set; } = null!;

    public List<User>? Users { get; set; } = new();
}