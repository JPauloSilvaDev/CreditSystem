using Credit.System.App.Mapper;
using Credit.System.App.Models;
using Credit.System.App.Repository;
using DataBase.Operations;
using DataBase.Operations.Interfaces;
using DataBase.Operations.Tables.ServiceSystem;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Credit.System.App.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IUserOperations _userOperations;
        private readonly ServiceSystemConnection _serviceSystemConnection;

        public CompanyController(IUserOperations userOperations, ServiceSystemConnection serviceSystemConnection)
        {
            _userOperations = userOperations;
            _serviceSystemConnection = serviceSystemConnection;

        }

        public IActionResult Index()
        {
            return View("CompanyRegister");
        }

        [HttpPost]
        public IActionResult CompanyRegister([FromBody] Company company)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                company.CreationDate = DateTime.Now;
               

                _serviceSystemConnection.Add(company);
                _serviceSystemConnection.SaveChanges();

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



    }
}
