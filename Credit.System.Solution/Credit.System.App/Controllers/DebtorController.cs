using Credit.System.App.CustomAttributes;
using Credit.System.App.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Platform.Utils;
using Platform.Utils.Validators;

namespace Credit.System.App.Controllers
{
    public class DebtorController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IDebtorOperations _debtorOperations;
        private readonly ICompanyOperations _companyOperations;

        public DebtorController(IConfiguration configuration, IDebtorOperations debtorOperations, ICompanyOperations companyOperations)
        {
            _configuration = configuration;
            _debtorOperations = debtorOperations;
            _companyOperations = companyOperations;
        }

        [CheckUserSession]
        public IActionResult Index()
        {
            UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));
            DebtorViewModel viewModel = new ();
            try
            {
                Company companyInfo = _companyOperations.GetCompanyById(userLogged.CompanyId);
                
                if (companyInfo.IsThirdPartyCollection)
                {
                    viewModel.Debtors = _debtorOperations.GetDebtorsByCompanyId(userLogged.CompanyId);
                }

                viewModel.Debtors = _debtorOperations.GetDebtorsByCompanyId(userLogged.CompanyId);

            }
            catch (CSException ex)
            {
                Json(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return View(viewModel);
        }

        [CheckUserSession]
        [HttpPost]
        public IActionResult RegisterDebtor([FromBody] RegisterDebtorModel debtorViewModel)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                Debtor debtor = null;/*ClientMapper.MapClientRegisterModelToClient(debtorViewModel);*/

                debtor.CompanyId = userLogged.CompanyId;

                if (!UtilsValidators.IsValidDocument(debtor.Document))
                    throw new CSException(CustomExceptionMessage.InvalidDocument);

                _debtorOperations.InsertDebtor(debtor);
            }

            catch (CSException exception)
            {
                return Json(new { success = false, message = exception.Message });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Json(new { success = true, message = CustomExceptionMessage.OperationSuccessMessage });

        }
        [CheckUserSession]
        [HttpPost]
        public IActionResult EditDebtor([FromBody] RegisterClientModel registerClientModel)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));
                Debtor debtor = null;
                debtor.CompanyId = userLogged.CompanyId;
                _debtorOperations.UpdateDebtor(debtor);
            }
            catch (Exception)
            {
                Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Json(new { success = true, message = CustomExceptionMessage.OperationSuccessMessage });
        }
        [CheckUserSession]
        [HttpPost]
        public IActionResult RemoveDebtor(long clientId)
        {
            try
            {
                _debtorOperations.DeleteDebtor(clientId);
            }
            catch (Exception)
            {
                Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Json(new { success = true, message = CustomExceptionMessage.OperationSuccessMessage });
        }
        [CheckUserSession]
        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Json(new { sucess = false, message = "Arquivo não pode ser vazio" });

            try
            {
                var folderPath = _configuration["BatchClientRegisterFilePath"];

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                BatchClientRegister batchClientRegister = new BatchClientRegister()
                {
                    FileName = file.FileName,
                    FilePath = filePath,
                };

                //_batchClientRegister.Insert(batchClientRegister);

                return Json(new { success = true, message = CustomExceptionMessage.OperationSuccessMessage });
            }
            catch (CSException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }
        }


    }
}
