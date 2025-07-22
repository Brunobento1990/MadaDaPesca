using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;
using MadaDaPesca.Infra.Enum;
using MadaDaPesca.Infra.HttpClient.Client;
using MadaDaPesca.Infra.Repositories;
using MadaDaPesca.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MadaDaPesca.Infra.DependencyInject;

public static class InfraDependencyInject
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IGuiaDePescaRepository, GuiaDePescaRepository>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ILoginGuiaDePescaRepository, LoginGuiaDePescaRepository>();
        return services;
    }

    public static IServiceCollection InjectDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
        return services;
    }

    public static IServiceCollection InjectHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var urlMarHttpClient = configuration["Api:OpenMeteo:Url"];
        var urlClimaHttpClient = configuration["Api:OpenMeteo:UrlClima"];

        services.AddHttpClient($"{HttpClientEnum.Open_meteo}", client =>
        {
            client.BaseAddress = new Uri(urlMarHttpClient!);
        });
        services.AddHttpClient($"{HttpClientEnum.Open_meteo_Clima}", client =>
        {
            client.BaseAddress = new Uri(urlClimaHttpClient!);
        });

        services.AddScoped<IMarHttpClient, MarHttpClient>();
        services.AddScoped<IClimaHttpClient, ClimaHttpClient>();
        return services;
    }
}
