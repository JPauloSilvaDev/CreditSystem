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
            //var userLogged = HttpContext.Session.GetString("UserLogged");

            //if (userLogged == null)
            //    return RedirectToAction("Index", "User");

            return View();
        }
        public IActionResult Register()
        {
            UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));
            

            if(userLogged.CompanyId == 1)
            {
                //inserir cliente na tabela Company
            }
            else
            {

            } //Inserir cliente na tabela companyclient.

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
