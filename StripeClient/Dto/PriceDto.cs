namespace StripeClient.Dto
{
    public class PriceDto
    {
        public string? Id { get; set; }
        public string? Interval { get; set; }
        public string? Currency { get; set; }
        public long? Amount { get; set; }
        public bool Active { get; set; }
    }
}
