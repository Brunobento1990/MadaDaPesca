using MadaDaPesca.Domain.Enum;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace MadaDaPesca.Application.Services;

public static class LogService
{
    public static void ConfigureLog(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.Seq(builder.Configuration["Seq:Url"]!)
            .CreateLogger();

        builder.Host.UseSerilog();
    }

    public static void LogApi(string mensagem, Exception? exception = null, TipoLogApiEnum tipoLogApiEnum = TipoLogApiEnum.Error)
    {
        switch (tipoLogApiEnum)
        {
            case TipoLogApiEnum.Debug:
                Log.Debug(mensagem, exception);
                break;
            case TipoLogApiEnum.Information:
                Log.Information(mensagem, exception);
                break;
            case TipoLogApiEnum.Warning:
                Log.Warning(mensagem, exception);
                break;
            case TipoLogApiEnum.Error:
                Log.Error(mensagem, exception);
                break;
            default:
                Log.Error(mensagem, exception);
                break;
        }
    }
}
