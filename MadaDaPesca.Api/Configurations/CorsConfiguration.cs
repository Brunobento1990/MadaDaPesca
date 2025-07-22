namespace MadaDaPesca.Api.Configurations;

public static class CorsConfiguration
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, string[] origins)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("base",
                builder => builder
                    .WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        return services;
    }
}
