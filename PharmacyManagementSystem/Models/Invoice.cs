using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagementSystem.Models
{
	public class Invoice
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }		
		public DateTime Date { get; set; }
		[Display(Name ="Customer name")]
		public string CustomerName { get; set; }
		[Display(Name = "Buying")]
		public bool IsBuy { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		[Display(Name = "Amount paid")]
		public decimal AmountPaied { get; set; }

		public virtual ICollection<InvoiceItems> InvoiceItems { get; set; }

		public Invoice()
		{
			InvoiceItems = new HashSet<InvoiceItems>();
		}
	}
}
