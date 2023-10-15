using Common.Models;
using Lab1_Data;
using Lab1_Web.Abstraction;
using Lab1_Web.Middlewares;
using Lab1_Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICommentService, CommentService>();

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
app.UseHttpsRedirection();

// Terminal middleware for user entities
app.UseUserEntityEndpoints();
// app.Use(async (context, next) =>
// {
//     var userService = context.RequestServices.GetService<IUserService>()
//                       ?? throw new Exception("User service not found");
//     var path = context.Request.Path;
//     switch (path)
//     {
//         case "/users":
//         {
//             var users = await userService.GetAllUsers();
//             await context.Response.WriteAsJsonAsync(users);
//             break;
//         }
//         case "/users/create":
//         {
//             var model = await context.Request.ReadFromJsonAsync<UserCreateModel>()
//                         ?? throw new Exception("Invalid user model");
//             var userId = await userService.CreateUser(model);
//             await context.Response.WriteAsJsonAsync(userId);
//             break;
//         }
//         case "/users/update":
//         {
//             var model = await context.Request.ReadFromJsonAsync<UserUpdateModel>()
//                         ?? throw new Exception("Invalid user model");
//             await userService.UpdateUser(model);
//             await context.Response.WriteAsync("User updated");
//             break;
//         }
//         case "/users/delete":
//         {
//             var model = await context.Request.ReadFromJsonAsync<UserDeleteModel>()
//                         ?? throw new Exception("Invalid user model");
//             await userService.DeleteUser(model);
//             await context.Response.WriteAsync("User deleted");
//             break;
//         }
//     }
//
//     await next.Invoke();
// });
//
// // Terminal middleware for article entities
// app.Use(async (context, next) =>
// {
//     var articleService = context.RequestServices.GetService<IArticleService>()
//                          ?? throw new Exception("Article service not found");
//     var path = context.Request.Path;
//     switch (path)
//     {
//         case "/articles":
//         {
//             var articles = await articleService.GetAllArticles();
//             await context.Response.WriteAsJsonAsync(articles);
//             break;
//         }
//         case "/articles/create":
//         {
//             var model = await context.Request.ReadFromJsonAsync<ArticleCreationModel>()
//                         ?? throw new Exception("Invalid article model");
//             var articleId = await articleService.CreateArticle(model);
//             await context.Response.WriteAsJsonAsync(articleId);
//             break;
//         }
//         case "/articles/update":
//         {
//             var model = await context.Request.ReadFromJsonAsync<ArticleUpdateModel>()
//                         ?? throw new Exception("Invalid article model");
//             await articleService.UpdateArticle(model);
//             await context.Response.WriteAsync("Article updated");
//             break;
//         }
//
//         case "/articles/delete":
//         {
//             var model = await context.Request.ReadFromJsonAsync<ArticleDeleteModel>()
//                         ?? throw new Exception("Invalid article model");
//             await articleService.DeleteArticle(model);
//             await context.Response.WriteAsync("Article deleted");
//             break;
//         }
//     }
//     await next.Invoke();
// });
//
// //Terminal middleware for comments entities
// app.Use(async (context, next) =>
// {
//     var commentService = context.RequestServices.GetService<ICommentService>()
//                          ?? throw new Exception("Comment service not found");
//     var path = context.Request.Path;
//     switch (path)
//     {
//         case "comments":
//         {
//             var comments = await commentService.GetAllComments();
//             await context.Response.WriteAsJsonAsync(comments);
//             break;
//         }
//         case "comments/create":
//         {
//             var model = await context.Request.ReadFromJsonAsync<CommentCreationModel>()
//                         ?? throw new Exception("Invalid comment model");
//             var commentId = await commentService.CreateComment(model);
//             await context.Response.WriteAsJsonAsync(commentId);
//             break;
//         }
//         case "comments/update":
//         {
//             var model = await context.Request.ReadFromJsonAsync<CommentUpdateModel>()
//                         ?? throw new Exception("Invalid comment model");
//             await commentService.UpdateComment(model);
//             await context.Response.WriteAsync("Comment updated");
//             break;
//         }
//         case "comments/delete":
//         {
//             var model = await context.Request.ReadFromJsonAsync<CommentDeleteModel>()
//                         ?? throw new Exception("Invalid comment model");
//             await commentService.DeleteComment(model);
//             await context.Response.WriteAsync("Comment deleted");
//             break;
//         }
//         case "comments/getForArticle":
//         {
//             var model = await context.Request.ReadFromJsonAsync<CommentGetForArticle>()
//                         ?? throw new Exception("Invalid comment model");
//             var comments = await commentService.GetCommentsForArticle(model);
//             await context.Response.WriteAsJsonAsync(comments);
//             break;
//         }
//         case "comments/getForAuthor":
//         {
//             var model = await context.Request.ReadFromJsonAsync<CommentGetForAuthor>()
//                         ?? throw new Exception("Invalid comment model");
//             var comments = await commentService.GetCommentsForAuthor(model);
//             await context.Response.WriteAsJsonAsync(comments);
//             break;
//         }
//     }
//     await next.Invoke();
// });


app.Run();