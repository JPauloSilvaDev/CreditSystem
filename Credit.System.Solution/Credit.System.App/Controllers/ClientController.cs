using Credit.System.App.CustomAttributes;
using Credit.System.App.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;

namespace Credit.System.App.Controllers
{
    [CheckUserSession]
    public class ClientController : Controller
    {
        private readonly IClientOperations _clientOperations;

        public ClientController(IClientOperations clientOperations)
        {
            _clientOperations = clientOperations;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        
        public IActionResult Register()
        {
            UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

            return View();

        }
        
        [HttpPost]
        public IActionResult RegisterClient([FromBody]Client client)   
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));
                client.CompanyId = userLogged.CompanyId;
                _clientOperations.InsertClient(client);
            }

            catch (CSException ex2)
            {
                Json(new { success = false, message = ex2.Message });
            }

            catch (Exception exc)
            {
                Json(new { success = false, message = exc.Message });
            }

            return Ok(new { success = true, message = "Cliente cadastrado com sucesso!" });

        }

    }
}
