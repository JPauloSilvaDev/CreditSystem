using System.Diagnostics.Eventing.Reader;
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
        private readonly IBatchDebtorRegisterOperations _batchDebtorRegisterOperations;

        public DebtorController(IConfiguration configuration, IDebtorOperations debtorOperations, ICompanyOperations companyOperations, IBatchDebtorRegisterOperations batchDebtorRegisterOperations)
        {
            _configuration = configuration;
            _debtorOperations = debtorOperations;
            _companyOperations = companyOperations;
            _batchDebtorRegisterOperations = batchDebtorRegisterOperations;
        }

        [CheckUserSession]
        public async Task<IActionResult> Index(DebtorViewModel viewModel)
        {
            return View(viewModel);
        }

        [CheckUserSession]
        [HttpPost]
        public async Task<IActionResult> RegisterDebtor([FromBody] RegisterDebtorModel debtorViewModel)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                Debtor debtor = new();/*ClientMapper.MapClientRegisterModelToClient(debtorViewModel);*/

                debtor.CompanyId = userLogged.CompanyId;

                if (!UtilsValidators.IsValidDocument(debtor.Document))
                    throw new CSException(CustomExceptionMessage.InvalidDocument);

                await _debtorOperations.InsertDebtorAsync(debtor);
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
        public async Task<IActionResult> EditDebtor([FromBody] RegisterClientModel registerClientModel)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));
                Debtor debtor = null;
                debtor.CompanyId = userLogged.CompanyId;
                await _debtorOperations.UpdateDebtorAsync(debtor);
            }
            catch (Exception)
            {
                Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Json(new { success = true, message = CustomExceptionMessage.OperationSuccessMessage });
        }
        [CheckUserSession]
        [HttpPost]
        public async Task<IActionResult> RemoveDebtor(long clientId)
        {
            try
            {
                await _debtorOperations.DeleteDebtorAsync(clientId);
            }
            catch (Exception)
            {
                Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Json(new { success = true, message = CustomExceptionMessage.OperationSuccessMessage });
        }
        [CheckUserSession]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
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

                BatchDebtorRegister batchDebtorRegister = new()
                {
                    FileName = file.FileName,
                    FilePath = filePath,
                };

                await _batchDebtorRegisterOperations.InsertBatchDebtorRegisterAsync(batchDebtorRegister);

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


        public async Task<IActionResult> GetDebtorsGrid(string filterType, string filterContent)
        {
            UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));
            DebtorViewModel viewModel = new();

            try
            {
                Company companyInfo = _companyOperations.GetCompanyById(userLogged.CompanyId);

                if (filterType == "Document")
                    viewModel.Debtors = await _debtorOperations.GetDebtorByDocumentAndCompanyId(filterContent, userLogged.CompanyId);

                else if (filterType == "Name")
                    viewModel.Debtors = await _debtorOperations.GetDebtorsByNameAndCompanyId(filterContent, userLogged.CompanyId);


                return PartialView("_DebtorsGridPartial", viewModel.Debtors);

            }
            catch (CSException ex)
            {
                Json(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }
            
            await Index(viewModel);

            return Json(new { success = true, message = ""});

        }

    }
}
