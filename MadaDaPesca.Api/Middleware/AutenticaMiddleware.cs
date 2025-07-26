using MadaDaPesca.Api.Attributes;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace MadaDaPesca.Api.Middleware;

public class AutenticaMiddleware
{
    private readonly RequestDelegate _next;

    public AutenticaMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext httpContext,
        IConfiguration configuration,
        IGuiaDePescaLogado guiaDePescaLogado,
        ITokenService tokenService,
        IGuiaDePescaRepository guiaDePescaRepository)
    {
        var autenticar = httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata
                .FirstOrDefault(m => m is AutenticaAttribute) is AutenticaAttribute;

        if (!autenticar)
        {
            await _next(httpContext);
            return;
        }

        var schema = httpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").FirstOrDefault()?.Replace("undefined", "");
        var token = httpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").LastOrDefault()?.Replace("undefined", "");
        ValidarSchema(schema, configuration);
        await ValidarToken(token, guiaDePescaLogado, tokenService, guiaDePescaRepository);

        await _next(httpContext);
    }

    private static void ValidarSchema(string? schema, IConfiguration configuration)
    {
        if (string.IsNullOrWhiteSpace(schema))
        {
            throw new ValidacaoException("Schema inválido", httpStatusCode: System.Net.HttpStatusCode.Unauthorized);
        }

        if (configuration["Jwt:Schema"] != schema)
        {
            throw new ValidacaoException($"Schema inválido, o schema deve ser o mesmo do retorno no login", httpStatusCode: System.Net.HttpStatusCode.Unauthorized);
        }
    }

    private static async Task ValidarToken(
        string? token,
        IGuiaDePescaLogado guiaDePescaLogado,
        ITokenService tokenService,
        IGuiaDePescaRepository guiaDePescaRepository)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ValidacaoException($"Informe o JWT de acesso", httpStatusCode: System.Net.HttpStatusCode.Unauthorized);
        }

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenService.Issue,
                ValidAudience = tokenService.Audience,
                IssuerSigningKey = tokenService.Key
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var id = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value
                ?? throw new ValidacaoException("Token inválido", System.Net.HttpStatusCode.Unauthorized);
            var isGuia = jwtToken.Claims.FirstOrDefault(c => c.Type == "Guia")?.Value?.ToUpper() == "TRUE";

            if (!Guid.TryParse(id, out Guid idParse))
            {
                throw new ValidacaoException("Por favor, efetue o login novamente", httpStatusCode: System.Net.HttpStatusCode.Unauthorized);
            }

            if (isGuia)
            {
                await ValidarAcessoGuiaDePesca(idParse, guiaDePescaRepository);
                guiaDePescaLogado.Id = idParse;
            }

        }
        catch (SecurityTokenExpiredException)
        {
            throw new ValidacaoException("Sessão expirada, efetue o login novamente!", httpStatusCode: System.Net.HttpStatusCode.Unauthorized);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static async Task ValidarAcessoGuiaDePesca(Guid id, IGuiaDePescaRepository guiaDePescaRepository)
    {
        var guiaDePesca = await guiaDePescaRepository.ObterParaValidarAcessoAsync(id)
            ?? throw new ValidacaoException("Não foi possível localizar seu cadastro para o acesso a API", httpStatusCode: System.Net.HttpStatusCode.Unauthorized);

        guiaDePesca.ValidarAcesso();
    }
}
