using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Repository.Entity
{
	[Table("aquelesite_prices")]
	[MySqlCharSet("utf8mb3")]
	[MySqlCollation("utf8mb3_general_ci")]

    // Representação dos Prices na conta Stripe no banco local
    public class Price
	{
		[Key]
		[Required]
		[Column("id", Order = 1)]
		public string Id { get; set; } = default!;

		[ForeignKey("product_id")]
		public virtual Product Product { get; set; } = null!;

		[DisplayName("Amount")]
		[Column("unit_amount", Order = 2)]
		public long UnitAmount { get; set; }

		[DisplayName("Currency")]
		[StringLength(5)]
		[Column("currency", Order = 3)]
		public string Currency { get; set; } = default!;

		[DisplayName("Type")]
		[StringLength(50)]
		[Column("type", Order = 4)]
		public string Type { get; set; } = "one_time";

		[DisplayName("Active")]
		[Column("active", Order = 5)]
		public bool Active { get; set; }

		[Column("created", Order = 6)]
		public DateTime Created { get; set; } = DateTime.UtcNow;

        [Column("updated", Order = 7)]
        public DateTime? Updated { get; set; }
    }
}
