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
        var assemblyApplication = Assembly.Load("MizeBazi.Store.Application"); 
        services.Scan(scan => scan
            .FromAssemblies(assemblyApplication)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IBehaviorHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
        servicesScanWithName(services, assemblyApplication);
        servicesScanWithName(services, Assembly.Load("MizeBazi.Store.Common"));
        servicesScanWithName(services, Assembly.Load("MizeBazi.Store.Domain"));

        servicesScanWithName(services, Assembly.Load("MizeBazi.Store.Data"));
        //servicesScanWithName(services, Assembly.Load("MizeBazi.Store.Extention"));
        servicesScanWithName(services, Assembly.Load("MizeBazi.Store.Services"));

        servicesScanWithName(services, Assembly.Load("MizeBazi.Store.Api"));

        services.AddScoped<IAppMediator, MediatorAdapter>();
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
    private static void servicesScanWithName(IServiceCollection services, Assembly ass)
    {
        services.Scan(scan => scan
            .FromAssemblies(ass)
            .AddClasses(classes => classes.Where(c =>
                c.IsClass &&
                !c.IsAbstract &&
                c.GetInterfaces().Any(i =>
                    i.Name.StartsWith("I") &&
                    i.Name.Substring(1) == c.Name)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
    }
}
