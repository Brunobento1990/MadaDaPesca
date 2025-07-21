using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MadaDaPesca.Application.DependencyInject;

public static class ApplicationDependencyInject
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateGuiaDePescaService, CreateGuiaDePescaService>();
        return services;
    }
}
