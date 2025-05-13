using Credit.System.App.CustomAttributes;
using Credit.System.App.Mapper;
using Credit.System.App.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Platform.Utils;
using Platform.Utils.Validators;

namespace Credit.System.App.Controllers
{
    [CheckUserSession]
    public class ClientController : Controller
    {
        private readonly IClientOperations _clientOperations;
        private readonly IConfiguration _configuration;
        private readonly IBatchClientRegisterOperations _batchClientRegister;
        
        public ClientController(IClientOperations clientOperations, IConfiguration configuration, IBatchClientRegisterOperations batchClientRegister)
        {
            _clientOperations = clientOperations;
            _configuration = configuration;
            _batchClientRegister = batchClientRegister;
        }

        public IActionResult Index()
        {
            UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

            List<Client>? clients = null;
            List<ClientViewModel>? viewModelClients = null;

            try
            {
                clients = _clientOperations.GetClientsByCompanyId(userLogged.CompanyId);

                if (clients == null)
                    return View(new List<ClientViewModel>());

                viewModelClients = ClientMapper.MapClientToClientViewModel(clients);

            }
            catch (Exception)
            {
                Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return View(viewModelClients);

        }

        [HttpPost]
        public IActionResult RegisterClient([FromBody] ClientViewModel clientViewModel)
        {
            try
            {
                UserSessionModel userLogged = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

                Client client = ClientMapper.MapClientViewModelToClient(clientViewModel);

                client.CompanyId = userLogged.CompanyId;

                if (!UtilsValidators.IsValidDocument(client.Document))
                    throw new CSException(CustomExceptionMessage.InvalidDocument);

                _clientOperations.InsertClient(client);
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
        
        [HttpPost]
        public IActionResult RemoveClient(long clientId)
        {
            try
            {
                _clientOperations.DeleteClient(clientId);
            }
            catch (Exception)
            {
                Json(new { success = false, message = CustomExceptionMessage.DefaultExceptionMessage });
            }

            return Json(new { success = true, message = CustomExceptionMessage.OperationSuccessMessage });
        }

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
                   
                _batchClientRegister.Insert(batchClientRegister);

                return Json(new { success = true, message = CustomExceptionMessage.OperationSuccessMessage }); 
            }
            catch (CSException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return Json(new {success = false, message = CustomExceptionMessage.DefaultExceptionMessage});
            }
        }

    }
}
