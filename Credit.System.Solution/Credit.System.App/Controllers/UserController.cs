using Credit.System.App.CustomAttributes;
using Credit.System.App.Mapper;
using Credit.System.App.Models;
using DataBase.Operations.Interfaces;
using DataBase.Operations.Tables.ServiceSystem;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Credit.System.App.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserOperations _userOperations;
        private readonly ICompanyOperations _companyOperations;
        
        public UserController(IUserOperations userOperations, ICompanyOperations companyOperations)
        {
            _userOperations = userOperations;
            _companyOperations = companyOperations;
        }

        public IActionResult Index()
        {
            return View();
        }
        [CheckUserSession]
        [HttpGet]
        public IActionResult Register()
        {

            UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

            if (userLogged.CompanyId == 1)
            {
                userLogged.CompanyList = _companyOperations.GetAllCompanies();
                
                return View(userLogged);
            }

            return View();
        }

        [CheckUserSession]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Register(RegisterUserModel userModel)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                if (userLogged.CompanyId != 1)
                    userModel.CompanyId = userLogged.CompanyId;

                bool userExists = _userOperations.UserExistsAtCompany(userModel.Login, userModel.CompanyId);

                if (userExists)
                    throw new CSException("Usuário já está cadastrado na empresa.");

                User newUser = UserMapper.MapRegisterUserModelToUser(userModel);

                _userOperations.InsertUser(newUser);
            }   
            catch (CSException exc)
            {
                JsonConvert.SerializeObject(new { success = "false", message = exc.Message });
            }  
            
            catch (Exception ex)
            {
                JsonConvert.SerializeObject(new { success = "false", message = ex.Message});
            }

            return JsonConvert.SerializeObject(new { success = "true", message = "Usuário cadastrado com sucesso!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string login, string password)
        {
            try
            {
                User user = _userOperations.GetUserByLoginAndPassword(login, password);

                if (user == null)
                    throw new CSException("Senha incorreta ou usuário não existe em nossa base de dados");

                var userModel = new { Id = user.UserId, Name = user.Name, CompanyId = user.CompanyId };

                string json = JsonConvert.SerializeObject(userModel);

                HttpContext.Session.SetString("UserLogged", json);

                return RedirectToAction("Index", "Home");

            }
            catch (CSException csEx)
            {
                return Json(new { success = "true", message = csEx.Message});
            }
            catch (Exception ex)
            {
                return Json(new { success = "true", message = "Não foi possível concluir a solicitação no momento, tente novamente mais tarde." });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
