namespace Lab1_Web.Middlewares;

public class UserEntityEndpoints
{
    private readonly RequestDelegate _next;

    public UserEntityEndpoints(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/users"))
        {
            await context.Response.WriteAsync("Hello from users");
        }
        else
        {
            await _next(context);
        }
    }
}

public static class UserEntityEndpointsExtensions
{
    public static IApplicationBuilder UseUserEntityEndpoints(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserEntityEndpoints>();
    }
}