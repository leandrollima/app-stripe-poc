using App.DTO.SuperClasses;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace App.DTO.ViewModels
{
	public class CheckoutResultViewModel : DtoModel
    {
        public string SessionId { get; set; } = default!;

        [DisplayName("Product name")]
        public string ProductName { get; set; } = default!;

        public string Currency { get; set; } = default!;

        [DisplayName("Amount")]
        public long Amount { get; set; }

        [DisplayName("Receipt url")]
        public string ReceiptUrl { get; set; } = default!;

        [HiddenInput]
        [DisplayName("Character")]
        public int PlayerId { get; set; }

        [DisplayName("Character name")]
        public string PlayerName { get; set; } = default!;

        public string? LinkAnimOutFit { get; set; }

        public string PaymentStatus { get; set; } = default!;
        public string PaymentUrl { get; set; } = default!;

        public bool MonthlyPackage { get; set; }    
    }
}
