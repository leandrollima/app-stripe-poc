using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Repository.Entity
{
	[Table("aquelesite_products")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]

    // Representação dos Products na conta Stripe no banco local
    public class Product
    { 
        [Key]
        [Required]
        [Column("id", Order = 1)]
        public string Id { get; set; } = default!;

        [DisplayName("Product name")]
        [StringLength(50)]
        [Column("product_name", Order = 2)]
        public string ProductName { get; set; } = default!;

		[DisplayName("Description")]
		[StringLength(150)]
        [Column("description", Order = 3)]
        public string Description { get; set; } = default!;

		[DisplayName("Active")]
		[Column("active", Order = 4)]
        public bool Active { get; set; }

        [DisplayName("Monthly package")]
        [Column("monthly_package", Order = 5)]
        public bool MonthlyPackage { get; set; }

        [DisplayName("Purchase limit")]
        [Column("purchase_limit", Order = 6)]
        public uint PurchaseLimit { get; set; }

        [DisplayName("Features")]
        [Column("features", Order = 7)]
        public string? Features { get; set; }
                		
        [Column("created", Order = 9)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [Column("updated", Order = 10)]
        public DateTime? Updated { get; set; }


        [DisplayName("Price")]
        [Column("price_id")]
        public virtual ICollection<Price> Prices { get; set; } = new List<Price>();

    }
}
