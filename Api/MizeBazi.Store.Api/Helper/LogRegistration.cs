
using Serilog;
using Serilog.Events;
using System.Runtime.InteropServices;

namespace MizeBazi.Store.Api.Helper;
public static class LogRegistration
{
    public static WebApplicationBuilder SetLogRegistration(this WebApplicationBuilder builder)
    {
        var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        var isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";

        string logBasePath;

        if (isProduction && isLinux)
        {
            logBasePath = "/var/log/myapp";  // لینوکس Production
        }
        else if (isLinux)
        {
            logBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "myapp", "logs"); // لینوکس Development
        }
        else
        {
            logBasePath = Path.Combine(AppContext.BaseDirectory, "Logs"); // ویندوز
        }
        Directory.CreateDirectory(Path.Combine(logBasePath, "All"));
        Directory.CreateDirectory(Path.Combine(logBasePath, "Errors"));

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "MyApp")

            // همه لاگ‌ها
            .WriteTo.File(
                path: Path.Combine(logBasePath, "All", "all-.txt"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
            )

            // فقط خطاها - 30 روزه
            .WriteTo.File(
                path: Path.Combine(logBasePath, "Errors", "error-.txt"),
                restrictedToMinimumLevel: LogEventLevel.Error,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                outputTemplate: "=== ERROR ==={NewLine}Time: {Timestamp:yyyy-MM-dd HH:mm:ss}{NewLine}Source: {SourceContext}{NewLine}Message: {Message}{NewLine}Exception: {Exception}{NewLine}================{NewLine}"
            )

            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();
        return builder;
    }

}

