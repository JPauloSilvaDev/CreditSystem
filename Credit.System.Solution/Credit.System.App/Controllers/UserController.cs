using Credit.System.App.CustomAttributes;
using Credit.System.App.Mapper;
using Credit.System.App.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Platform.Utils;
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
                userLogged.CompanyList = _companyOperations.GetAllCompanies();

            return View(userLogged);
        }

        [CheckUserSession]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([FromBody] RegisterUserModel userModel)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                if (userModel.CompanyId == null)
                    userModel.CompanyId = userLogged.CompanyId;

                bool userExists = _userOperations.UserExistsAtCompany(userModel.Login, userModel.CompanyId.Value);

                if (userExists)
                    throw new CSException("Usuário já está cadastrado na empresa.");

                User newUser = UserMapper.MapRegisterUserModelToUser(userModel);

                _userOperations.InsertUser(newUser);
            }
            catch (CSException exc)
            {
                return StatusCode(422, new { success = false, message = exc.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }

            return Ok(new { success = true, message = "Usuário cadastrado com sucesso!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string login, string password)
        {
            try
            {
                User user = _userOperations.GetUserByLoginAndPassword(login, password);

                if (user == null)
                    throw new CSException(CustomExceptionMessage.UserMessage0001);

                UserSessionModel sessionModel = new UserSessionModel()
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    CompanyId = user.CompanyId,
                    Email = user.Email,
                };

                string json = JsonConvert.SerializeObject(sessionModel);

                HttpContext.Session.SetString("UserLogged", json);

                return RedirectToAction("Index", "Home");

            }
            catch (CSException csEx)
            {
                return Json(new { success = "true", message = csEx.Message });
            }
            catch (Exception)
            {
                return Json(new { success = "true", message = CustomExceptionMessage.GenericMessage0001, });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
