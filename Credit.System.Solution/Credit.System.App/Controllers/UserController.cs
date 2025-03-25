using Credit.System.App.Repository;
using DataBase.Operations;
using DataBase.Operations.Tables.ServiceSystem;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Utils.Security;

namespace Credit.System.App.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;
        private readonly DataBaseConnection _dataBaseConnection;

        public UserController(ILogger<UserController> logger, IConfiguration configuration, DataBaseConnection dataBaseConnection)
        {
            _logger = logger;
            _configuration = configuration;
            _dataBaseConnection = dataBaseConnection;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User userData)
        {
            try
            {
                new UserOperations(_dataBaseConnection).InsertUser(userData);
            }
            catch (Exception)
            {

            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string login, string password)
        {
            try
            {
                string encryptedPassword = Security.Encrypt(password);

                User user = _dataBaseConnection.User.Where(x => x.Login == login && x.Password == encryptedPassword).FirstOrDefault();

                if (user == null)
                {
                    throw new Exception("Senha incorreta ou usuário não existe em nossa base de dados");
                }

                var userModel = new { Id = user.UserId, Name = user.Name, CompanyId = user.CompanyId };

                string json = JsonConvert.SerializeObject(userModel);

                HttpContext.Session.SetString("UserLogged", json);

                return RedirectToAction("Index", "Home");

            }
            catch (Exception)
            {
                throw;
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
