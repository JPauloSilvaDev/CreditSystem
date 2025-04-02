using Microsoft.AspNetCore.Mvc;

namespace Credit.System.App.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View("CompanyRegister");
        }
    }
}
