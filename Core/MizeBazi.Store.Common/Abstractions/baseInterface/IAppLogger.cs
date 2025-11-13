namespace MizeBazi.Store.Common.Abstractions;
public interface IAppLogger<T>
{
    void LogInformation(string message);
    void LogInformation<T0>(string message, T0 arg0);
    void LogInformation<T0, T1>(string message, T0 arg0, T1 arg1);

    void LogWarning(string message);
    void LogWarning<T0>(string message, T0 arg0);

    void LogError(string message);
    void LogError(Exception exception, string message);
    void LogError<T0>(Exception exception, string message, T0 arg0);

    void LogDebug(string message);

    IDisposable BeginScope<TState>(TState state);
}