using Stripe;
using Stripe.Checkout;
using StripeClient.Dto;

namespace StripeClient
{
    public class CheckoutStripeClient
    {
        private readonly SessionService _sessionService;
        private readonly PaymentIntentService _paymentIntentService;
        private readonly ChargeService _chargeService;

        public CheckoutStripeClient(SessionService sessionService,
            PaymentIntentService paymentIntentService,
            ChargeService chargeService)
        {
            _sessionService = sessionService;
            _paymentIntentService = paymentIntentService;
            _chargeService = chargeService;
        }

        public Session PaymentWithValueFree(string productId, string currency, long unitAmount, string successUrl, string cancelUrl, string mode, string paymentMethod)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                  {
                    paymentMethod
                  },
                LineItems = new List<SessionLineItemOptions>
                  {
                    new SessionLineItemOptions
                    {
                      PriceData = new SessionLineItemPriceDataOptions
                      {
                        Product = productId,
                        UnitAmount = unitAmount,
                        Currency = currency,
                      },
                      Quantity = 1,
                    },
                  },
                Mode = mode,
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
            };

            Session session = _sessionService.Create(options);

            return session;
        }

        public Session ProductPayment(string priceId, string successUrl, string cancelUrl)
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
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            Session session = _sessionService.Create(options);

            return session;
        }

        /// <summary>
        /// Get Stripe Session register
        /// </summary>
        /// <param name="session_id"></param>
        /// <returns></returns>
        public SessionDto GetSession(string session_id)
        {
            Session session = _sessionService.Get(session_id);
            string? receiptUrl = null;

            if (!string.IsNullOrWhiteSpace(session.PaymentIntentId))
            {
				var paymentIntent = _paymentIntentService.Get(session.PaymentIntentId);
				var lastCharge = _chargeService.Get(paymentIntent?.LatestChargeId);

				receiptUrl = lastCharge?.ReceiptUrl;
			}

			var dto = new SessionDto
            {
                Id = session.Id,
                CheckoutStatus = session.Status,
                CustomerDetails = session.CustomerDetails,
                PaymentStatus = session.PaymentStatus,
                ReceiptUrl = receiptUrl,
                Currency = session.Currency,
                PaymentUrl = session.Url
            };

            return dto;
        }
    }
}
