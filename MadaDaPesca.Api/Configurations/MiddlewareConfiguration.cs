using MadaDaPesca.Api.Middleware;

namespace MadaDaPesca.Api.Configurations;

public static class MiddlewareConfiguration
{
    public static void UseMiddlewareConfiguration(this IApplicationBuilder app)
    {
        app.UseMiddleware<LogMiddleware>();
    }
}
