using Credit.System.App.Repository;
using DataBase.Operations;
using DataBase.Operations.Tables.ServiceSystem;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Utils.Security;
using Utils;
using DataBase.Operations.Interfaces;
namespace Credit.System.App.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceSystemConnection _dataBaseConnection;

        public UserController(IConfiguration configuration, ServiceSystemConnection dataBaseConnection)
        {
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
                return View(ViewBag["Error"] = "Não foi possível atender a solicitação no momento.");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string login, string password)
        {
            try
            {
                User user = new UserOperations(_dataBaseConnection).GetUserByLoginAndPassword(login, password);

                if (user == null)
                    throw new Exception("Senha incorreta ou usuário não existe em nossa base de dados");

                var userModel = new { Id = user.UserId, Name = user.Name, CompanyId = user.CompanyId };

                string json = JsonConvert.SerializeObject(userModel);

                HttpContext.Session.SetString("UserLogged", json);

                return RedirectToAction("Index", "Home");

            }
            catch (Exception)
            {
                return Json(new { });
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
