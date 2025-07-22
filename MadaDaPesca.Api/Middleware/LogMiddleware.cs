using MadaDaPesca.Application.Adapters;
using MadaDaPesca.Application.Services;
using MadaDaPesca.Domain.Enum;
using MadaDaPesca.Domain.Exceptions;

namespace MadaDaPesca.Api.Middleware;

public class LogMiddleware
{
    private readonly RequestDelegate _next;

    public LogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
    {
        try
        {
            await _next(context);
        }
        catch (ValidacaoException ex)
        {
            LogService.LogApi("Erro de validação", exception: ex, tipoLogApiEnum: TipoLogApiEnum.Information);
            await HandleError(context, ex.Erro, statusCode: (int?)ex.HttpStatusCode ?? 400);
        }
        catch (Exception ex)
        {
            LogService.LogApi("Erro inesperado", exception: ex);
            var erro = ex.InnerException?.Message ?? ex.Message;
            var erroReal = configuration["Ambiente"]?.ToUpper() != "PRODUCAO" ? erro
                : "Ocorreu um erro inesperado. Tente novamente mais tarde ou entre em contato com o suporte.";

            var erroResponse = new ErrorViewModel
            {
                Erros = new List<Error> { new Error { Erro = erroReal } }
            };
            await HandleError(context, erroResponse, 500);
        }
    }

    public async Task HandleError(HttpContext httpContext, ErrorViewModel errorViewModel, int statusCode = 400)
    {
        httpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializerAdapters.Serialize(errorViewModel));
    }
}
