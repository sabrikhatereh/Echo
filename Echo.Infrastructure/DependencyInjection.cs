using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Echo.Application;
using Echo.Application.Abstractions.DbContexts;
using Echo.Application.Behaviors;
using Echo.Core;
using Echo.Core.Abstractions;
using Echo.Core.Abstractions.Services;
using Echo.Infrastructure.Persistence;
using Echo.Infrastructure.Persistence.DbContexts;
using Echo.Infrastructure.Repositories.CommandRepositories;
using Echo.Infrastructure.Repositories.QueryRepositories;
using Echo.Infrastructure.Services;
using System.Reflection;

namespace Echo.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add write DbContext
            services.AddDbContext<EchoWriteDbContext>(opt =>
            opt.UseInMemoryDatabase(databaseName: "Echodb")
            .EnableSensitiveDataLogging(true)
            );

            // Register interfaces for read and write contexts
            services.AddScoped<IApplicationReadDb, ApplicationReadDb>();
            services.AddScoped<IApplicationWriteDbContext, EchoWriteDbContext>();
            services.TryAddScoped<IUnitOfWork, UnitOfWork>();


            services.AddEasyCaching(options => { options.UseInMemory(configuration, "mem"); });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
           
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(Assembly.GetAssembly(typeof(ApplicationLayer))!);
                configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                configuration.AddRequestPreProcessor(typeof(RequestValidationBehavior<>));
            });
            services.RegisterRepositories();
            services.RegisterService();
            
        }
        private static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IHashUniqueness, HashUniqueness>();
            services.TryAddScoped<IPostRateLimitService, PostRateLimitService>();
            services.AddSingleton<IForbidWords, ForbidWords>();
        }
        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEchoCommandRepository, EchoCommandRepository>();
            services.AddScoped<IEchoQueryRepository, EchoQueryRepository>();
        }
    }
}
