using Serilog;

namespace MadaDaPesca.Application.Services;

public static class LogService
{
    public static void ConfigureLog(string url)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq(url)
            .CreateLogger();
    }
}
