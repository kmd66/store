using MizeBazi.Store.Common.Abstractions;

namespace MizeBazi.Store.Common.Shared;

public static class ServiceLocator
{
    private static IServiceProvider _provider;

    public static void Configure(IServiceProvider provider)
    {
        _provider = provider;
    }

    public static IEventDispatcher Dispatcher =>
        (IEventDispatcher)(_provider?.GetService(typeof(IEventDispatcher))
        ?? throw new InvalidOperationException("ServiceLocator not configured."));
}