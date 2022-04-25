using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public virtual DbSet<Item> Items { get; set; }
		public virtual DbSet<Invoice> Invoices { get; set; }
		public virtual DbSet<InvoiceItems> InvoiceItems { get; set; }

		public virtual DbSet<user> Users { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
				: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<InvoiceItems>()
				.HasKey(k => new { k.InvoiceId, k.ItemId });

			base.OnModelCreating(builder);
		}
	}
}