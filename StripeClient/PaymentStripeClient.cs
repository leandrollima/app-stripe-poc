using Microsoft.Extensions.Options;
using StripeClient.Settings;
using Stripe.Checkout;

namespace StripeClient
{
    public class PaymentStripeClient
    {
        private readonly SessionService _sessionService;
        private readonly StripeTokensSettings _settings;

        public PaymentStripeClient(SessionService sessionService, IOptions<StripeTokensSettings> settings)
        {
            _sessionService = sessionService;
            this._settings = settings.Value;
        }

        public async Task<string> CreateIntentAsync(string customerId, string priceId, string successUrl, string cancelUrl, bool trial)
        {
            try
            {
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            Price = priceId,
                            Quantity = 1,
                        },
                    },
                    Mode = "subscription",
                    SuccessUrl = successUrl,
                    CancelUrl = cancelUrl,
                    AutomaticTax = new SessionAutomaticTaxOptions { Enabled = false },
                    Customer = customerId,
                    CustomerUpdate = new SessionCustomerUpdateOptions {
                        Shipping = "auto",
                        Name = "auto",
                        Address = "auto"
                    },
                };

                if(trial)
                {
                    options.SubscriptionData = new SessionSubscriptionDataOptions
                    {
                        TrialEnd = DateTime.Now.AddDays(_settings.TrialDays + 1)
                    };
                }

                Session session = await _sessionService.CreateAsync(options);

                return session.Url;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
