using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Lab3.Data;
using Lab3.Areas.Identity.Data;
using Lab3.Services;
using Lab3.Abstraction;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'Lab3ContextConnection' not found.");

builder.Services.AddDbContext<Lab3Context>(options => options.UseSqlite(connectionString));
builder.Services.AddRazorPages();
builder.Services.AddDefaultIdentity<Lab3User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<Lab3Context>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    return new SqlConnectionFactory(configuration);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
