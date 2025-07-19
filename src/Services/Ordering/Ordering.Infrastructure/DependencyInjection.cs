using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;

namespace Ordering.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        // Add services to the container.
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });




        // we can not use this any more beacures we are passing an Mediator class which i s the diaspatchdomanievents so we go through the list of the services in the Isavechangesionterceptors 

        //services.AddDbContext<ApplicationDbContext>((sp, options) =>
        //{
        //    options.AddInterceptors(new AuditableEntityInterceptor(),  new sdkhjabgdkfayhbhdfas);
        //    options.UseSqlServer(connectionString);
        //});

        // must be implemnted in the applicationdbcontext
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}