using Credit.System.App.CustomAttributes;
using Credit.System.App.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Platform.Utils;

namespace Credit.System.App.Controllers
{
    public class DebtorController : Controller
    {

        private readonly IClientOperations _clientOperations;
        private readonly IDebtorOperations _debtorOperations;
        private readonly ICompanyOperations _companyOperations;

        public DebtorController(IClientOperations clientOperations, IDebtorOperations debtorOperations, ICompanyOperations companyOperations)
        {
            _clientOperations = clientOperations;
            _debtorOperations = debtorOperations;
            _companyOperations = companyOperations;
        }

        [CheckUserSession]
        public IActionResult Index()
        {
            UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));
            DebtorViewModel viewModel = new DebtorViewModel();
            try
            {
                Company companyInfo = _companyOperations.GetCompanyById(userLogged.CompanyId);
                
                if (companyInfo.IsThirdPartyCollection)
                {
                    viewModel.Clients = _clientOperations.GetClientsByCompanyId(userLogged.CompanyId);
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
    }
}
