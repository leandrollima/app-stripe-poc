namespace App.DTO.ViewModels
{
    public class PaymentDto
    {
        public string SessionId { get; set; } = default!;
        public uint PlayerId { get; set; }
        public string ProductName { get; set; } = default!;
        public string PriceId { get; set; } = default!;
        public long Amount { get; set; }
        public string Currency { get; set; } = default!;
        public string ChargeId { get; set; } = default!;
        public string BalanceTransationId { get; set; } = default!;
        public string ReceiptUrl { get; set; } = default!;
        public bool Paid { get; set; } = false;
        public string CheckoutStatus { get; set; } = default!;
        public string PaymentStatus { get; set; } = default!;
        public string PaymentUrl { get; set; } = default!;
        public string ProductId { get; set; } = default!;
        public string? Coupon { get; set; }
    }
}
