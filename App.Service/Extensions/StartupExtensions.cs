using App.Repository.Configuration;
using App.Service.SettingsModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StripeClient.Extensions;

namespace App.Service.Extensions
{
	public static class StartupExtensions
    {
        public static IServiceCollection AddContextAndRepository(this IServiceCollection serviceCollection, string stringConnection, string sqlVersion)
        {
            serviceCollection
            .AddAppDbContext(stringConnection, sqlVersion)
            .AddRepositories();

            return serviceCollection;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<PaymentService>();
            services.AddTransient<ProductService>();
            services.AddTransient<ImportProductsStripeService>();
            return services;
        }

        public static IServiceCollection AddSettingsModels(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CouponSettings>(configuration.GetSection("Coupon"));

            return services;
        }

        public static IServiceCollection AddStripeConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            int.TryParse(configuration.GetSection("Stripe")["TrialDays"]!.ToString(), out int trialDaysStripe);

            services.InitializeStripeConfigurations(configuration.GetSection("Stripe")["TokenConnectedAccountId"]!.ToString(),
               configuration.GetSection("Stripe")["SecretKey"]!.ToString(),
               configuration.GetSection("Stripe")["PublishableKey"]!.ToString(),
               trialDaysStripe);
            return services;
        }

        public static WebApplication UseStripe(this WebApplication app)
        {
            var stripeSettings = app.Services.GetService<StripeClient.Settings.StripeSettings>();
            StartupStripe.UseStripe(stripeSettings!.SecretKey);
            return app;
        }
    }
}
