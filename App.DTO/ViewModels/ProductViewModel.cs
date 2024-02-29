using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.DTO.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; } = default!;

        [DisplayName("Product name")]
        [StringLength(50)]
        public string ProductName { get; set; } = default!;

        [DisplayName("Description")]
        [StringLength(150)]
        public string Description { get; set; } = default!;

        public string PriceId { get; set; } = default!;

        [DisplayName("Amount")]
        public long PriceAmount { get; set; }

        [DisplayName("Amount")]
        public string PriceAmountFormatted { get { return GetFormattedPrice(); } }

        [DisplayName("Currency")]
        [StringLength(5)]
        public string Currency { get; set; } = default!;

        [DisplayName("Features")]
        public string[]? Features { get; set; }

        public bool MonthlyPackage { get; set; }

        private string GetFormattedPrice()
        {
            if (Currency == "brl")
                return string.Format(new System.Globalization.CultureInfo("pt-BR"), "{0:C2}", PriceAmount / 100.0);
            else
                return string.Format(new System.Globalization.CultureInfo("en-US"), "{0:C2}", PriceAmount / 100.0);
        }
    }
}
