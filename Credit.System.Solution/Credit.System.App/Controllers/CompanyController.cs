using Credit.System.App.Models;
using DataBase.Operations.Interfaces;
using DataBase.Operations.Tables.ServiceSystem;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Credit.System.App.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyOperations _companyOperations;

        public CompanyController(ICompanyOperations companyOperations)
        {
            _companyOperations = companyOperations;
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

                _companyOperations.InsertCompany(company);

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
