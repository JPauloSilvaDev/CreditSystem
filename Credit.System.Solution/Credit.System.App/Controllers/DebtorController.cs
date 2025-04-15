using Microsoft.AspNetCore.Mvc;

namespace Credit.System.App.Controllers
{
    public class DebtorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
