using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagementSystem.Models
{
	public class Item
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public Decimal Price { get; set; }
		public string? Image { get; set; }

		public virtual ICollection<InvoiceItems> InvoiceItems { get; set; }

		public Item()
		{
			InvoiceItems = new HashSet<InvoiceItems>();
		}
	}
}
