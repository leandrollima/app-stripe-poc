using App.Repository.Context;
using App.Repository.Entity;
using App.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repository.Configuration
{
	public static class RepositoryConfiguration
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection serviceCollection, string stringConnection, string mySqlVersion)
        {
            serviceCollection.AddDbContextFactory<AppDbContext>(options =>
            {
                options.UseMySql(stringConnection,
                    ServerVersion.Create(
                        new Version(mySqlVersion),
                        Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql)
                    , builder =>
                    {
                        builder.EnableRetryOnFailure();
                    });

                options.EnableSensitiveDataLogging(true);
            });

            return serviceCollection;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Player>, EFRepository<Player>>();
            services.AddTransient<IRepository<Payment>, EFRepository<Payment>>();
            services.AddTransient<IRepository<Product>, EFRepository<Product>>();
            services.AddTransient<IRepository<Price>, EFRepository<Price>>();

            return services;
        }
    }
}
