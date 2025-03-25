using System.Diagnostics;
using Credit.System.App.Models;
using Credit.System.App.Repository;
using Microsoft.AspNetCore.Mvc;


namespace Credit.System.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataBaseConnection _context;

        public HomeController(DataBaseConnection context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userLogged = HttpContext.Session.GetString("UserLogged");

            if (userLogged == null)
                return RedirectToAction("Index", "User");

            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}






