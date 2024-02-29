using App.DTO.SuperClasses;
using System.ComponentModel;

namespace App.DTO.ViewModels
{
    public class CouponViewModel : DtoModel
    {
        [DisplayName("Coupon")]
        public string Id { get; set; } = default!;

        [DisplayName("Partner")]
        public string Partner { get; set; } = default!;

        [DisplayName("Url")]
        public string? Url { get; set; }

        [DisplayName("Coins")]
        public int Coins { get; set; }

        [DisplayName("Active")]
        public bool Active { get; set; }
    }
}
