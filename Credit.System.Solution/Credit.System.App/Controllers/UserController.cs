using Credit.System.App.Repository;
using Credit.System.App.TableModels.ServiceSystem;
using Credit.System.App.Utils;
using CreditSystem.Utilities.ConfigHelper;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Register(User userData)
        {
            
            //check if user email/cpf exist

            //save new user into the table with the company id being the one of the logged user or if the logged user is admin he can create from any company.

            
            return View();
        }

        public IActionResult Login(string login, string password)
        {
            try
            {
                string key = _configuration["SecretKey"];

                string encryptedPassword = Security.Encrypt(password, key);

                User user = _dataBaseConnection.User.Where(x => x.Login == login && x.Password == encryptedPassword).FirstOrDefault();

                if (user == null)
                {
                    //return custom exception
                }


                //start user session

            }
            catch (Exception)
            {

                throw;
            }
           
            
            return View();
        }

        public IActionResult Logout() { 
        
            return RedirectToAction("Index", "Home");
        }
    }
}
