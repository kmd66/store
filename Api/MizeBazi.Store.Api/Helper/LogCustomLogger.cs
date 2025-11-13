using MizeBazi.Store.Common.Abstractions;

namespace MizeBazi.Store.Api.Helper;

public class SerilogAppLogger<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;

    public SerilogAppLogger(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message)
    {
        _logger.LogInformation(message);
    }

    public void LogInformation<T0>(string message, T0 arg0)
    {
        _logger.LogInformation(message, arg0);
    }

    public void LogInformation<T0, T1>(string message, T0 arg0, T1 arg1)
    {
        _logger.LogInformation(message, arg0, arg1);
    }

    public void LogWarning(string message)
    {
        _logger.LogWarning(message);
    }

    public void LogWarning<T0>(string message, T0 arg0)
    {
        _logger.LogWarning(message, arg0);
    }

    public void LogError(string message)
    {
        _logger.LogError(message);
    }

    public void LogError(Exception exception, string message)
    {
        _logger.LogError(exception, message);
    }

    public void LogError<T0>(Exception exception, string message, T0 arg0)
    {
        _logger.LogError(exception, message, arg0);
    }

    public void LogDebug(string message)
    {
        _logger.LogDebug(message);
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return _logger.BeginScope(state);
    }
}