using System.Diagnostics;
using Credit.System.App.Models;
using Credit.System.App.Repository;
using CreditSystem.Utilities;
using CreditSystem.Utilities.ConfigHelper;
using DataBase.Operations;
using DataBase.Operations.Tables.ServiceSystem;
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
            User users = _context.User.FirstOrDefault();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}






