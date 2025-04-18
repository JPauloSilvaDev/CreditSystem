﻿using System;
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
                Json(new { success = false, message = CustomExceptionMessage.GenericMessage0001 });
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
                    throw new CSException();

                _clientOperations.InsertClient(client);
            }

            catch (CSException exception)
            {
                Json(new { success = false, message = exception.Message });
            }

            catch (Exception)
            {
                Json(new { success = false, message = CustomExceptionMessage.GenericMessage0001 });
            }

            return Ok(new { success = true, message = "Cliente cadastrado com sucesso!" });

        }

        public IActionResult RemoveClient([FromBody] Client client)
        {
            try
            {
                _clientOperations.DeleteClient(client);
            }
            catch (Exception)
            {
                Json(new { success = false, message = CustomExceptionMessage.GenericMessage0001 });
            }

            return Json(new { success = true, message = string.Empty});
        }
    
    }
}
