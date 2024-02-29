namespace StripeClient.Settings
{
    public class StripeSettings
    {
        public string SecretKey { get; set; } = default!;
        public string PublishableKey { get; set; } = default!;
        public string WebhookKey { get; set; } = default!;
    }
}
