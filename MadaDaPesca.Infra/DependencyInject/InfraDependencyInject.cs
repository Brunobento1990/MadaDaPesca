using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;
using MadaDaPesca.Infra.Repositories;
using MadaDaPesca.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MadaDaPesca.Infra.DependencyInject;

public static class InfraDependencyInject
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IGuiaDePescaRepository, GuiaDePescaRepository>();
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }

    public static IServiceCollection InjectDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
        return services;
    }
}
