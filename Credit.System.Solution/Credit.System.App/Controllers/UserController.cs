using Credit.System.App.Repository;
using Credit.System.App.TableModels.ServiceSystem;
using Microsoft.AspNetCore.Mvc;

namespace Credit.System.App.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;
        private readonly DataBaseConnection _context;

        public UserController(ILogger<UserController> logger, IConfiguration configuration, DataBaseConnection context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string login, string password)
        {
            
            User user = _context.User.Where(x=> x.Login == login && x.Password == _configuration.Getpassword).FirstOrDefault();
            
            return View();
        }


    }
}
