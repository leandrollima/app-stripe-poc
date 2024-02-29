using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Repository.Entity
{
    [Table("aquelesite_payments")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public class Payment
    {
        [Key]
        [Required]
        [Column("id")]
        public string Id { get; set; } = default!;

        [Column("player_id", TypeName = "int(11) unsigned")]
        public uint PlayerId { get; set; }

        [StringLength(150)]
        [Column("product_name")]
        public string ProductName { get; set; } = default!;

        [StringLength(50)]
        [Column("product_id")]
        public string ProductId { get; set; } = default!;

        [StringLength(50)]
        [Column("price_id")]
        public string PriceId { get; set; } = default!;

        [Column("amount")]
        public long Amount { get; set; }

        [Column("currency")]
        public string Currency { get; set; } = default!;

        [Column("paid")]
        public bool Paid { get; set; } = false;

        [Column("coupon_id")]
        public string? Coupon { get; set; }

        [StringLength(50)]
        [Column("checkout_status")]
        public string CheckoutStatus { get; set; } = default!;

        [StringLength(50)]
        [Column("payment_status")]
        public string PaymentStatus { get; set; } = default!;

        [Column("receipt_url")]
        [StringLength(300)]
        public string? ReceiptUrl { get; set; }


        [Column("created")]
        public DateTime Created { get; set; }

        [Column("updated")]
        public DateTime? Updated { get; set; }
    }
}
