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

            RegisterUserModel registerModel = new RegisterUserModel();

            registerModel.CompanyId = userLogged.CompanyId;

            if (userLogged.CompanyId == 1)
                registerModel.CompanyList = _companyOperations.GetAllCompanies();


            List<User> userList = _userOperations.GetAllUsersByCompanyId(userLogged.CompanyId);

            List<UserViewModel> userViewModelList = UserMapper.MapUserListToUserViewModelList(userList);

            registerModel.Users = userViewModelList;

            return View(registerModel);
        }

        [CheckUserSession]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([FromBody] RegisterUserModel userModel)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                #region Verificando se o usuário logado pertence a empresa do sistema
                if (userModel.CompanyId == null)
                    userModel.CompanyId = userLogged.CompanyId;

                #endregion


                bool userExists = _userOperations.UserExistsAtCompany(userModel.Login, userModel.CompanyId);

                if (userExists)
                    throw new CSException(string.Format(CustomExceptionMessage.UserMessage0000, userModel.Login));

                User newUser = UserMapper.MapRegisterUserModelToUser(userModel);

                _userOperations.InsertUser(newUser);
            }
            catch (CSException exc)
            {
                return Json(new { success = false, message = exc.Message });
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Ok(new { success = true, message = "Usuário cadastrado com sucesso!" });
        }


        [CheckUserSession]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser([FromBody] UserViewModel userViewModel)
        {
            try
            {
                User user = UserMapper.MapUserViewModelToUser(userViewModel);

                _userOperations.UpdateUser(user);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.GenericMessage0001 });
            }
           

            return Json(new { success = true, message = "Alterações salvas com sucesso."});
        }

        [CheckUserSession]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveUser([FromBody] UserViewModel userViewModel)
        {
            try
            {
                _userOperations.DeleteUser(userViewModel.UserId);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.GenericMessage0001});
            }

            return Json(new { success = true, message = CustomExceptionMessage.SaveChangesMessage });
        }
      


        [HttpPost]
        public IActionResult Login([FromBody] LoginModel loginInfo)
        {
            try
            {
                User user = _userOperations.GetUserByLoginAndPassword(loginInfo.Login, loginInfo.Password);

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

                return Json(new { success = true, message = string.Empty });

            }
            catch (CSException csEx)
            {
                return Json(new { success = false, message = csEx.Message });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.GenericMessage0001, });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
