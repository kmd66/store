using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Data;
using MizeBazi.Store.Services;

namespace MizeBazi.Store.Api.Helper;
public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        EfDependency(services);


        services.AddScoped<IEventDispatcher, EventDispatcher>();
        services.AddSingleton<IMessageBus, RabbitMQMessageBus>();

        return services;
    }
    private static void EfDependency(IServiceCollection services)
    {
        services.AddScoped<AppSaveChanges>();

        services.AddDbContext<StoreContext>((sp, options) =>
        {
            options.UseSqlServer(AppSetings.WriteConnection, sql =>
            {
                sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.AddInterceptors(sp.GetRequiredService<AppSaveChanges>());
        });
    }
}
