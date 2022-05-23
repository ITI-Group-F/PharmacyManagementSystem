using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Data;

namespace PharmacyManagementSystem.Controllers
{
	[Authorize(Roles = "Admin")]
	public class DashboardController : Controller
	{
		private readonly ApplicationDbContext _context;

		public IActionResult Index()
		{
			return View();
		}
	}
}
