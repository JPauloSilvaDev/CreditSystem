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

        public ClientController(IClientOperations clientOperations)
        {
            _clientOperations = clientOperations;
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
        public IActionResult BatchCompanyRegister(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "No file was uploaded." });
            }

            // Validate file extension
            var allowedExtensions = new[] { ".xls", ".xlsx" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return Json(new { success = false, message = "Invalid file type. Only .xls or .xlsx allowed." });
            }

            // Define the path to save the uploaded file
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            // Create the folder if it doesn't exist
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Build the file path
            var filePath = Path.Combine(uploadsFolder, Path.GetFileName(file.FileName));

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Json(new { success = true, message = CustomExceptionMessage.OperationSuccessMessage });
        }




    }
}
