using Hangfire;
using Hangfire.PostgreSql;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.Models;
using MadaDaPesca.Application.Services;
using MadaDaPesca.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MadaDaPesca.Application.DependencyInject;

public static class ApplicationDependencyInject
{
    public static IServiceCollection InjectServices(this IServiceCollection services, bool configurarHangFire)
    {
        if (configurarHangFire)
        {
            services.AddHangfireServer();
        }

        services.AddScoped<IGuiaDePescaService, GuiaDePescaService>();
        services.AddScoped<ILoginGuiaDePescaService, LoginGuiaDePescaService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMarService, MarService>();
        services.AddScoped<IHomeGuiaDePescaService, HomeGuiaDePescaService>();
        services.AddScoped<IGuiaDePescaLogado, GuiaDePescaLogado>();
        services.AddScoped<IClimaService, ClimaService>();
        services.AddScoped<IPescariaService, PescariaService>();
        services.AddScoped<IAgendaPescariaService, AgendaPescariaService>();
        services.AddScoped<IConfirmarContaGuiaDePescaService, ConfirmarContaGuiaDePescaService>();
        services.AddScoped<IEsqueceuSenhaGuiaDePescaService, EsqueceuSenhaGuiaDePescaService>();
        services.AddScoped<IRecuperarSenhaGuiaDePescaService, RecuperarSenhaGuiaDePescaService>();
        services.AddScoped<IEmbarcacaoService, EmbarcacaoService>();
        services.AddScoped<INotificacaoAgendamentoService, NotificacaoAgendamentoService>();
        services.AddScoped<IFaturaAgendaPescariaService, FaturaAgendaPescariaService>();
        services.AddScoped<IGaleriaDeTrofeuService, GaleriaDeTrofeuService>();
        services.AddScoped<IHomePescadorService, HomePescadorService>();

        return services;
    }

    public static IServiceCollection ConfigurarHangFire(this IServiceCollection services, string conectionString)
    {
        services.AddHangfire(config => config.UsePostgreSqlStorage(opt =>
        {
            opt.UseNpgsqlConnection(conectionString);
        }));

        return services;
    }

    public static void ConfigurarDashBoardHangFire(this WebApplication app)
    {
        app.UseHangfireDashboard();

        using var scope = app.Services.CreateScope();
        var jobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

        jobManager.AddOrUpdate<INotificacaoAgendamentoService>(
            "notificar-agenda-pescaria",
            service => service.NotificarAsync(),
            Cron.Daily(7, 0)
        );
    }

    public static IServiceCollection InjectJwt(this IServiceCollection services, string key, string issue, string audience)
    {
        services.AddAuthentication(
            JwtBearerDefaults.AuthenticationScheme).
            AddJwtBearer(options =>
             options.TokenValidationParameters = OptionsToken(key, issue, audience));

        return services;
    }

    private static TokenValidationParameters OptionsToken(string key, string issue, string audience)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidAudience = audience,
            ValidIssuer = issue,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(key))
        };
    }
}
