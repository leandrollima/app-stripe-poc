using App.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stripe;
using StripeClient;

namespace App.Web.MVC.Controllers
{
    [Route("webhook")]
    [ApiController]
    public class WebhookController : Controller
    {
        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        const string endpointSecret = "whsec_....";
        private readonly CheckoutStripeClient _checkoutService;
        private readonly PaymentService _paymentService;

        public WebhookController(CheckoutStripeClient checkoutService,
            PaymentService paymentService)
        {
            _checkoutService = checkoutService;
            _paymentService = paymentService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                Event? stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                Console.WriteLine("\n\n**********************************************************************************************");
                Console.WriteLine($"stripeEvent event type: {stripeEvent.Type}: {JsonConvert.SerializeObject(stripeEvent.Data.RawObject, Formatting.Indented)}");

                // Handle the event
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    if (stripeEvent.Data.RawObject.TryGetValue("id", out JToken idToken))
                    {
                        var session = await _paymentService.GetSessionAsync(idToken?.Value<string>()!);
                        if (session is not null)
                        {
                            session.Paid = true;

                            stripeEvent.Data.RawObject.TryGetValue("payment_status", out JToken paymentStatus);
                            stripeEvent.Data.RawObject.TryGetValue("status", out JToken status);

                            session.PaymentStatus = paymentStatus?.Value<string>()!;
                            session.CheckoutStatus = status?.Value<string>()!;
                            await _paymentService.UpdateStripePaymentOnWebhookAsync(session);
                        }
                    }

                }
                // ... handle other event types
                else
                {
                    //Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
