using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Data;

namespace Blogvio.WebApi.Data
{
	public static class PrebDb
	{
		private static ApplicationDbContext _context;

		public static void PrepSqlServerDatabase(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.CreateScope())
			{
				MigrateSqlServer(serviceScope.ServiceProvider.GetService<ApplicationDbContext>());
				_context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
			}
		}

		private static void MigrateSqlServer(ApplicationDbContext context)
		{
			try
			{
				context.Database.Migrate();
			}
			catch (Exception ex)
			{
			}
		}

		public static async Task SeedAdminUser()
		{
			var user = new IdentityUser
			{
				UserName = "admin@mail.com",
				NormalizedUserName = "admin@mail.com",
				Email = "admin@mail.com",
				NormalizedEmail = "admin@mail.com",
				EmailConfirmed = false,
				LockoutEnabled = true,
				SecurityStamp = Guid.NewGuid().ToString()
			};

			var roleStore = new RoleStore<IdentityRole>(_context);

			if (!_context.Roles.Any(r => r.Name == "Admin"))
			{
				await roleStore.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "admin" });
			}

			if (!_context.Users.Any(u => u.UserName == user.UserName))
			{
				var password = new PasswordHasher<IdentityUser>();
				var hashed = password.HashPassword(user, "Pass#word!");
				user.PasswordHash = hashed;
				var userStore = new UserStore<IdentityUser>(_context);
				var result = await userStore.CreateAsync(user);
				if (result.Succeeded)
				{
					await userStore.AddToRoleAsync(user, "User");
					await userStore.AddToRoleAsync(user, "Admin");
				}
			}
			await _context.SaveChangesAsync();
		}

	}
}