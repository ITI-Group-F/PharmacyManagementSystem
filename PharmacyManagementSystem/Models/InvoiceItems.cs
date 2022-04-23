using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagementSystem.Models
{
	public class InvoiceItems
	{
		[Key]
		[ForeignKey("Invoice")]
		public int InvoiceId { get; set; }
		[Key]
		[ForeignKey("Item")]
		public int ItemId { get; set; }
		public int Amount { get; set; }

		public virtual Invoice Invoice { get; set; }
		public virtual Item Item { get; set; }
	}
}
