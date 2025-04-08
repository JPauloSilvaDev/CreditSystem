using Credit.System.App.Models;
using Platform.Entity.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.Entity.ServiceSystem;

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
                
                //Criar e chamar classe validator, validando os campos obrigatórios, CPF e CNPJ.
                
                _companyOperations.InsertCompany(company);

            }
            catch (CSException exc)
            {
                return StatusCode(422, new { success = false, message = exc.Message });
            }

            catch (Exception)
            {
                throw;
            }

            return Ok(new { success = true, message = "Cliente cadastrado com sucesso!" });
        }
    
    }
}
