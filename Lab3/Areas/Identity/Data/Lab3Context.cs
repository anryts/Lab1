using Lab1_Web.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using Lab3.Entities;
using Lab3User = Lab3.Areas.Identity.Data.Lab3User;

namespace Lab3.Data;

public class Lab3Context : IdentityDbContext<Lab3User>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Gender> Genders { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public Lab3Context(DbContextOptions<Lab3Context> options)
        : base(options)
    {
    }
}
