using Microsoft.Extensions.DependencyInjection;
using Stripe;
using StripeClient.Settings;

namespace StripeClient.Extensions
{
    public static class StartupStripe
    {
        private const int TRIAL_DAYS = 15;
        private const int MINIMUM_TRIAL_DAYS_ACCEPTED_BY_STRIPE_API = 3;


		public static void UseStripe(string secretKey)
		{
			StripeConfiguration.ApiKey = secretKey;
		}

		public static IServiceCollection InitializeStripeConfigurations(this IServiceCollection services, string stripeTokenConnectedAccountId, string secretKey, string secretPublishableKey, int trialDaysStripe)
        {
            services.AddStripeConfiguration(
              secretKey,
              secretPublishableKey
              );

            services.AddStripeTokensConfiguration(
                stripeTokenConnectedAccountId,
                trialDaysStripe
                );

            services.AddStripeServicesConfiguration();
            return services;
        }
        private static IServiceCollection AddStripeConfiguration(this IServiceCollection services, string secretKey, string secretPublishableKey)
        {
            services.AddSingleton(new StripeSettings() { SecretKey = secretKey, PublishableKey = secretPublishableKey });
            return services;
        }

        private static IServiceCollection AddStripeTokensConfiguration(this IServiceCollection services,
            string connectedAccountId, int trialDays)
        {
            services.AddSingleton(new StripeTokensSettings
            {
                ConnectedAccountId = connectedAccountId,
                TrialDays = trialDays <= MINIMUM_TRIAL_DAYS_ACCEPTED_BY_STRIPE_API ? TRIAL_DAYS : trialDays
            });

            return services;
        }

        private static IServiceCollection AddStripeServicesConfiguration(this IServiceCollection services)
        {
            // stripe lib services            
            services.AddScoped<CustomerService>();
            services.AddScoped<InvoiceService>();
            services.AddScoped<PaymentIntentService>();
            services.AddScoped<ProductService>();
            services.AddScoped<PriceService>();
            services.AddScoped<SubscriptionService>();
            services.AddScoped<Stripe.Checkout.SessionService>();
            services.AddScoped<Stripe.BillingPortal.SessionService>();
            services.AddScoped<ChargeService>();


            // my services
            services.AddScoped<PaymentStripeClient>();
            services.AddScoped<ProductStripeClient>();
            services.AddScoped<CheckoutStripeClient>();

            return services;
        }

    }
}
