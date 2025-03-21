using System.Diagnostics;
using Credit.System.App.Models;
using Credit.System.App.Repository;
using Credit.System.App.TableModels.ServiceSystem;
using Microsoft.AspNetCore.Mvc;


namespace Credit.System.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly DataBaseConnection _context;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, DataBaseConnection context)
        {
            _logger = logger;
            _configuration = configuration;
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






