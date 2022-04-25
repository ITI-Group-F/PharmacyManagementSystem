using Microsoft.AspNetCore.Mvc;

namespace PharmacyManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
