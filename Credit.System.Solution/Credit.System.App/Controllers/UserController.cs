using Credit.System.App.Repository;
using DataBase.Operations;
using DataBase.Operations.Tables.ServiceSystem;
using Microsoft.AspNetCore.Mvc;
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
           //validate existing session and redirect to login 
            return View();
        }

        public IActionResult Register(User userData)
        {
            try
            {
                //validator class
                new UserOperations(_dataBaseConnection).InsertUser(userData);
                
            }
            catch (Exception)
            {
              
            }
            
            //check if user email/cpf exist

            //save new user into the table with the company id being the one of the logged user or if the logged user is admin he can create from any company.

            
            return View();
        }

        public IActionResult Login(string login, string password)
        {
            try
            {
                string encryptedPassword = Security.Encrypt(password);

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
