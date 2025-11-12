namespace MizeBazi.Store.Common.Shared;

public static class ServiceLocator
{
    private static IServiceProvider _provider;

    public static void Configure(IServiceProvider provider)
    {
        _provider = provider;
    }

    public static IServiceProvider Dispatcher =>
        (IServiceProvider)(_provider?.GetService(typeof(IServiceProvider))
        ?? throw new InvalidOperationException("ServiceLocator not configured."));
}