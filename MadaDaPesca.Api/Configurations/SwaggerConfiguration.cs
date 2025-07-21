namespace MadaDaPesca.Api.Configurations;

internal static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Mada da Pesca API",
                Version = "v1",
                Description = "API for managing fishing guides and related entities."
            });
        });
        return services;
    }
}
