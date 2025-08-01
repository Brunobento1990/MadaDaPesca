using MadaDaPesca.Domain.Enum;
using Serilog;

namespace MadaDaPesca.Application.Services;

public static class LogService
{
    public static void ConfigureLog(string url)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Warning()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq(url)
            .CreateLogger();
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
