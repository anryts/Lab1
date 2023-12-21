using Common.Models;
using Lab1_Data;
using Lab1_Web.Abstraction;
using Lab1_Web.Controllers;
using Lab1_Web.Middlewares;
using Lab1_Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    return new SqlConnectionFactory(configuration);
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab4_Web v1"));
}

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Lab 4
app.MapEndpoints();

app.UseHttpsRedirection();

// app.UseUserEntityEndpoints();
// app.UseArticleEntityEndpoints();
// app.UseCommentEntityEndpoints();

app.Run();