using Credit.System.App.CustomAttributes;
using Credit.System.App.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Credit.System.App.Controllers
{
    [CheckUserSession]
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

            return View();

        }
        public IActionResult GetClientsList()
        {

            //var userLogged = HttpContext.Session.GetString("UserLogged");

            //if (userLogged == null)
            //    return RedirectToAction("Index", "User");

            return View();//model do cliente
        }

    }
}
