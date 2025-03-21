using Microsoft.AspNetCore.Mvc;

namespace Credit.System.App.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Login()
        {
            return View();
        }


    }
}
