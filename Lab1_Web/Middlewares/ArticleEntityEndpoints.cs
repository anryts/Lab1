using Common.Models;
using Lab1_Web.Services;

namespace Lab1_Web.Middlewares;

public class ArticleEntityEndpoints
{
    private readonly RequestDelegate _next;

    public ArticleEntityEndpoints(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var articleService = context.RequestServices.GetService<IArticleService>()
                             ?? throw new Exception("Cannot resolve IArticleService");

        var path = context.Request.Path;
        switch (path)
        {
            case "/articles":
            {
                var articles = await articleService.GetAllArticles();
                await context.Response.WriteAsJsonAsync(articles);
                break;
            }
            case "/articles/create":
            {
                var model = await context.Request.ReadFromJsonAsync<ArticleCreationModel>()
                            ?? throw new Exception("Invalid article model");
                var articleId = await articleService.CreateArticle(model);
                await context.Response.WriteAsJsonAsync(articleId);
                break;
            }
            case "/articles/update":
            {
                var model = await context.Request.ReadFromJsonAsync<ArticleUpdateModel>()
                            ?? throw new Exception("Invalid article model");
                await articleService.UpdateArticle(model);
                await context.Response.WriteAsync("Article updated");
                break;
            }

            case "/articles/delete":
            {
                var model = await context.Request.ReadFromJsonAsync<ArticleDeleteModel>()
                            ?? throw new Exception("Invalid article model");
                await articleService.DeleteArticle(model);
                await context.Response.WriteAsync("Article deleted");
                break;
            }
        }

        await _next.Invoke(context);
    }
}

public static class ArticleEntityEndpointsExtensions
{
    public static IApplicationBuilder UseArticleEntityEndpoints(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ArticleEntityEndpoints>();
    }
}