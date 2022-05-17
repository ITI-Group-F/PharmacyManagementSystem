using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PharmacyManagementSystem.Data.Migrations;

namespace PharmacyManagementSystem.Data
{
	public class Seeding : ISeeding
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;

		public Seeding(ApplicationDbContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task SeedAdminUser()
		{
			var user = new IdentityUser
			{
				UserName = "admin@mail.com",
				Email = "admin@mail.com",
			};
			try
			{
				var result = await _userManager.CreateAsync(user, "Pass#word5");
				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "Admin");
				}
			}
			catch (Exception ex)
			{

			}
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
