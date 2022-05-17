using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PharmacyManagementSystem.Data.Migrations;

namespace PharmacyManagementSystem.Data
{
	public class Seeding : ISeeding
	{
		private readonly ApplicationDbContext _context;

		public Seeding(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task SeedAdminUser()
		{
			var user = new IdentityUser
			{
				UserName = "admin@mail.com",
				NormalizedUserName = "admin@mail.com",
				Email = "admin@mail.com",
				NormalizedEmail = "admin@mail.com",
				EmailConfirmed = false,
				LockoutEnabled = false,
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
				await userStore.CreateAsync(user);
				await userStore.AddToRoleAsync(user, "User");
				await userStore.AddToRoleAsync(user, "Admin");
			}

			await _context.SaveChangesAsync();
		}
	}
}
