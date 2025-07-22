using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.Models;
using MadaDaPesca.Application.Services;
using MadaDaPesca.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MadaDaPesca.Application.DependencyInject;

public static class ApplicationDependencyInject
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IGuiaDePescaService, GuiaDePescaService>();
        services.AddScoped<ILoginGuiaDePescaService, LoginGuiaDePescaService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMarService, MarService>();
        services.AddScoped<IHomeGuiaDePescaService, HomeGuiaDePescaService>();
        services.AddScoped<IGuiaDePescaLogado, GuiaDePescaLogado>();
        return services;
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
