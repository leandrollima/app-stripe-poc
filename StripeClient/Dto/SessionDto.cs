using Stripe.Checkout;

namespace StripeClient.Dto
{
    public class SessionDto
    {
        public string Id { get; set; } = default!;
		public string PaymentStatus { get; set; } = default!;
        public string CheckoutStatus { get; set; } = default!;
        public SessionCustomerDetails? CustomerDetails { get; set; }
        public string? ReceiptUrl { get; internal set; }
        public string Currency { get; internal set; } = default!;
        public string PaymentUrl { get; internal set; } = default!;
    }
}
