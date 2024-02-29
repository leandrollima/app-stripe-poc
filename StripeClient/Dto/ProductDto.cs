namespace StripeClient.Dto
{
    public class ProductDto
    {
        public string Id { get; set; } = default!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool MonthlyPackage { get; set; }
        public uint PurchaseLimit { get; set; }
        public bool Active { get; set; }

        public List<string> Features { get; set; } = new List<string>();

        public ICollection<PriceDto>? Prices { get; set; }
    }
}
