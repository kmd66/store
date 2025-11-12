using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Application;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Data;
using MizeBazi.Store.Domain;
using MizeBazi.Store.Services;
using System.Reflection;

namespace MizeBazi.Store.Api.Helper;
public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        EfDependency(services);



        // ثبت MediatR فقط در WebAPI
        var appAssembly = Assembly.Load("MizeBazi.Store.Application"); 
        services.Scan(scan => scan
            .FromAssemblies(appAssembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IBehaviorHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        services.AddScoped<IAppMediator, MediatorAdapter>();


        services.AddTransient<IDomainEventHandler<OrderConfirmedEvent>, OrderConfirmedEventHandler>();

        services.AddScoped<IEventDispatcher, EventDispatcher>();
        services.AddScoped<AutoEventDispatcher>();

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
