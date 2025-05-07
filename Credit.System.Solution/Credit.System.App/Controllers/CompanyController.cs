using Credit.System.App.Models;
using Platform.Entity.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.Entity.ServiceSystem;
using Credit.System.App.CustomAttributes;
using Platform.Utils;
using Platform.Utils.Validators;
using NPOI.XSSF.UserModel;


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
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

        }

        [HttpPost]
        public IActionResult CompanyRegister([FromBody] Company company)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                if (!UtilsValidators.IsValidDocument(company.Document))
                    throw new CSException(CustomExceptionMessage.InvalidDocument);


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

            return Json(new { success = true, message = "Cliente cadastrado com sucesso!" });
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

                _companyOperations.UpdateCompany(company);

            }
            catch (CSException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Json(new { success = true, message = "Cliente cadastrado com sucesso!" });
        }





        //public List<Company> ImportCompaniesFromExcel(string filePath)
        //{
        //    var companies = new List<Company>();

        //    using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //    {
        //        var workbook = new XSSFWorkbook(file);
        //        var sheet = workbook.GetSheetAt(0); // first sheet
        //        int rowCount = sheet.LastRowNum;

        //        for (int i = 1; i <= rowCount; i++) // skip header row (i = 1)
        //        {
        //            var row = sheet.GetRow(i);
        //            if (row == null) continue;

        //            var company = new Company
        //            {
        //                CompanyId = long.TryParse(row.GetCell(0)?.ToString(), out var id) ? id : 0,
        //                PrimaryName = row.GetCell(1)?.ToString(),
        //                SecondaryName = row.GetCell(2)?.ToString(),
        //                Document = row.GetCell(3)?.ToString(),
        //                PrimaryPhone = row.GetCell(4)?.ToString(),
        //                SecondaryPhone = row.GetCell(5)?.ToString(),
        //                Email = row.GetCell(6)?.ToString(),
        //                ZipCode = row.GetCell(7)?.ToString(),
        //                AddressNumber = short.TryParse(row.GetCell(8)?.ToString(), out var num) ? num : (short?)null,
        //                City = row.GetCell(9)?.ToString(),
        //                State = row.GetCell(10)?.ToString(),
        //                Observation = row.GetCell(11)?.ToString(),
        //                Neighborhood = row.GetCell(12)?.ToString()
        //            };

        //            companies.Add(company);
        //        }
        //    }

        //    return companies;
        //}



    }
}
