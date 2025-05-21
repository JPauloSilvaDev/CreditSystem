using Credit.System.App.CustomAttributes;
using Credit.System.App.Mapper;
using Credit.System.App.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Platform.Utils;
using Platform.Utils.Validators;
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
        public async Task<IActionResult> Register()
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                RegisterUserModel registerModel = new ();

                registerModel.CompanyId = userLogged.CompanyId;

                if (userLogged.CompanyId == 1)
                    registerModel.CompanyList = _companyOperations.GetAllCompanies();

                List<User> userList = await _userOperations.GetAllUsersByCompanyIdAsync(userLogged.CompanyId);

                List<UserViewModel> userViewModelList = UserMapper.MapUserListToUserViewModelList(userList);

                registerModel.Users = userViewModelList;

                return View(registerModel);
            }
            catch (Exception)
            {
               return Json(new {sucess = false, Message = CustomExceptionMessage.DefaultExceptionMessage});
            }
            
          
        }

        [CheckUserSession]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel userModel)
        {
            try
            {
                if (!UtilsValidators.IsValidDocument(userModel.Login))
                    throw new CSException(CustomExceptionMessage.InvalidDocument);

                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                if (userModel.CompanyId == null)
                    userModel.CompanyId = userLogged.CompanyId;

                User newUser = UserMapper.MapRegisterUserModelToUser(userModel);

                await _userOperations.InsertUserAsync(newUser);
            }
            catch (CSException exc)
            {
                return Json(new { success = false, message = exc.Message });
            }

            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage});
            }

            return Json(new { success = true, message = CustomExceptionMessage.OperationSuccessMessage});
        }


        [CheckUserSession]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser([FromBody] UserViewModel userViewModel)
        {
            try
            {
                User user = UserMapper.MapUserViewModelToUser(userViewModel);

               await _userOperations.UpdateUserAsync(user);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }
           

            return Json(new { success = true, message = "Alterações salvas com sucesso."});
        }

        [CheckUserSession]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUser([FromBody] UserViewModel userViewModel)
        {
            try
            {
               await _userOperations.DeleteUserAsync(userViewModel.UserId);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Json(new { success = true, message = CustomExceptionMessage.SaveChangesMessage });
        }
      


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginInfo)
        {
            try
            {
                User user = await _userOperations.GetUserByLoginAndPasswordAsync(loginInfo.Login, loginInfo.Password);

                if (user == null)
                    throw new CSException(CustomExceptionMessage.UserMessage0001);

                UserSessionModel sessionModel = new()
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
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage, });
            }
        }
        
        [CheckUserSession]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockUserAccess(long userId)
        {
            try
            {
               await _userOperations.BlockUserAccessAsync(userId);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Json(new {success = true, message = CustomExceptionMessage.OperationSuccessMessage}); 
        }
       
        [CheckUserSession]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnblockUserAccess(long userId)
        {
            try
            {
                await _userOperations.UnblockUserAccessAsync(userId);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Json(new { success = true, message = CustomExceptionMessage.OperationSuccessMessage}); //message bloqueado acesso do usuário
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
