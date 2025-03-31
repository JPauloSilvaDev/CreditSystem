using Credit.System.App.CustomAttributes;
using Microsoft.AspNetCore.Mvc;

namespace Credit.System.App.Controllers
{
    [CheckUserSession]
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            //var userLogged = HttpContext.Session.GetString("UserLogged");

            //if (userLogged == null)
            //    return RedirectToAction("Index", "User");

            return View();
        }
        public IActionResult Register()
        {
            //var userLogged = HttpContext.Session.GetString("UserLogged");

            //if (userLogged == null)
            //    return RedirectToAction("Index", "User");

            return View();//new client

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
