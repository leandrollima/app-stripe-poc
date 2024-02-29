namespace StripeClient.Settings
{
    public class StripeTokensSettings
    {
        public int TrialDays { get; set; } = 15;
        public string ConnectedAccountId { get; set; } = default!;        
    }
}
