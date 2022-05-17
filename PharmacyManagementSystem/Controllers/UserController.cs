using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.ViewsModels;
using System.Security.Cryptography;

namespace PharmacyManagementSystem.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;

		public UserController(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _userManager.Users.ToListAsync());
		}

		public async Task<IActionResult> Details(string? id)
		{
			if (id is null)
			{
				return NotFound();
			}
			var user = await _userManager.FindByIdAsync(id);
			if (user is null)
			{
				return NotFound();
			}
			return View(user);
		}

		public async Task<IActionResult> Create([Bind("UserName, Email, Password")] UserOpt model)
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser { Email = model.Email, UserName = model.Email };
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "User");
					return RedirectToAction(nameof(Index));
				}
			}
			return View(model);
		}

		public async Task<IActionResult> Edit(string? id)
		{
			if (id is null)
			{
				return NotFound();
			}

			var item = await _userManager.FindByIdAsync(id);
			if (item is null)
			{
				return NotFound();
			}
			item.PasswordHash = String.Empty;
			return View(item);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,PasswordHash")] IdentityUser model)
		{
			if (id != model.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByIdAsync(id);
					user.UserName = model.UserName;
					user.Email = model.Email;
					user.PasswordHash = HashPassword(model.PasswordHash);
					await _userManager.UpdateAsync(user);
				}
				catch (DbUpdateConcurrencyException) { }
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		public async Task<IActionResult> Delete(string? id)
		{
			if (id is null)
			{
				return NotFound();
			}

			var user = await _userManager.FindByIdAsync(id);
			if (user is null)
			{
				return NotFound();
			}

			return View(user);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			await _userManager.DeleteAsync(user);
			return RedirectToAction(nameof(Index));
		}

		private string HashPassword(string password)
		{
			byte[] salt;
			byte[] buffer2;
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
			{
				salt = bytes.Salt;
				buffer2 = bytes.GetBytes(0x20);
			}
			byte[] dst = new byte[0x31];
			Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
			Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
			return Convert.ToBase64String(dst);
		}
	}
}
