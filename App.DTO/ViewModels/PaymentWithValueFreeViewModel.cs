namespace App.DTO.ViewModels
{
    public class PaymentWithValueFreeViewModel
    {
        public string PaymentMethod { get; set; } = "card";
        public string Mode { get; set; } = "payment";
        public string Currency { get; set; } = "brl";

        // in cents
        public long UnitAmount { get; set; }

        public string SuccessUrl { get; set; } = default!;
        public string CancelUrl { get; set; } = default!;
    }
}
