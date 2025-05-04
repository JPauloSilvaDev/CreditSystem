using Credit.System.App.Models;
using Platform.Entity.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.Entity.ServiceSystem;
using Credit.System.App.CustomAttributes;
using Platform.Utils;
using Platform.Utils.Validators;

namespace Credit.System.App.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyOperations _companyOperations;

        public CompanyController(ICompanyOperations companyOperations)
        {
            _companyOperations = companyOperations;
        }
        [CheckUserSession]
        public IActionResult Index()
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));
                
                Company company = _companyOperations.GetCompanyById(userLogged.CompanyId);

                return View(company);

            }
            catch (Exception)
            {
                return Json(new {success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }
           
        }

        [HttpPost]
        public IActionResult CompanyRegister([FromBody] Company company)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                if (!UtilsValidators.IsValidDocument(company.Document))
                    throw new CSException("Documento informado é inválido");
                //Criar e chamar classe validator, validando os campos obrigatórios, CPF e CNPJ.
                
                _companyOperations.InsertCompany(company);

            }
            catch (CSException ex)
            {
                return Json(new { success = false, message = ex.Message});
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Ok(new { success = true, message = "Cliente cadastrado com sucesso!" });
        }



        [HttpPost]
        public IActionResult EditCompany([FromBody] Company company)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                if (!UtilsValidators.IsValidDocument(company.Document))
                    throw new CSException("Documento informado é inválido");
                
                //Criar e chamar classe validator, validando os campos obrigatórios, CPF e CNPJ.

                _companyOperations.InsertCompany(company);

            }
            catch (CSException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Ok(new { success = true, message = "Cliente cadastrado com sucesso!" });
        }


    }
}
