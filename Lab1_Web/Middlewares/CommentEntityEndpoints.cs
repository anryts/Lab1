using Common.Models;
using Lab1_Web.Services;

namespace Lab1_Web.Middlewares;

public class CommentEntityEndpoints
{
    private readonly RequestDelegate _next;

    public CommentEntityEndpoints(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var commentService = context.RequestServices.GetService<ICommentService>()
                             ?? throw new Exception("Cannot resolve ICommentService");

        var path = context.Request.Path;
        switch (path)
        {
            case "comments":
            {
                var comments = await commentService.GetAllComments();
                await context.Response.WriteAsJsonAsync(comments);
                break;
            }
            case "comments/create":
            {
                var model = await context.Request.ReadFromJsonAsync<CommentCreationModel>()
                            ?? throw new Exception("Invalid comment model");
                var commentId = await commentService.CreateComment(model);
                await context.Response.WriteAsJsonAsync(commentId);
                break;
            }
            case "comments/update":
            {
                var model = await context.Request.ReadFromJsonAsync<CommentUpdateModel>()
                            ?? throw new Exception("Invalid comment model");
                await commentService.UpdateComment(model);
                await context.Response.WriteAsync("Comment updated");
                break;
            }
            case "comments/delete":
            {
                var model = await context.Request.ReadFromJsonAsync<CommentDeleteModel>()
                            ?? throw new Exception("Invalid comment model");
                await commentService.DeleteComment(model);
                await context.Response.WriteAsync("Comment deleted");
                break;
            }
            case "comments/getForArticle":
            {
                var model = await context.Request.ReadFromJsonAsync<CommentGetForArticle>()
                            ?? throw new Exception("Invalid comment model");
                var comments = await commentService.GetCommentsForArticle(model);
                await context.Response.WriteAsJsonAsync(comments);
                break;
            }
            case "comments/getForAuthor":
            {
                var model = await context.Request.ReadFromJsonAsync<CommentGetForAuthor>()
                            ?? throw new Exception("Invalid comment model");
                var comments = await commentService.GetCommentsForAuthor(model);
                await context.Response.WriteAsJsonAsync(comments);
                break;
            }
        }

        await _next.Invoke(context);
    }

}

public static class CommentEntityEndpointsExtensions
{
    public static IApplicationBuilder UseCommentEntityEndpoints(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CommentEntityEndpoints>();
    }
}