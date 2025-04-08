using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Credit.System.App.Models;
using System;

namespace Credit.System.App.Controllers
{
    public class TituloController : Controller
    {
        public IActionResult Index()
        {
            // Simulando dados do banco de dados
            var titulos = new List<TituloViewModel>
            {
                new TituloViewModel 
                { 
                    Id = 1, 
                    Numero = "001", 
                    Cliente = "Cliente A", 
                    Valor = 1000.00m, 
                    Vencimento = new DateTime(2024, 03, 01), 
                    Status = "Pendente" 
                },
                new TituloViewModel 
                { 
                    Id = 2, 
                    Numero = "002", 
                    Cliente = "Cliente B", 
                    Valor = 2000.00m, 
                    Vencimento = new DateTime(2024, 03, 15), 
                    Status = "Pago" 
                },
                new TituloViewModel 
                { 
                    Id = 3, 
                    Numero = "003", 
                    Cliente = "Cliente C", 
                    Valor = 3000.00m, 
                    Vencimento = new DateTime(2024, 04, 01), 
                    Status = "Pendente" 
                },
                new TituloViewModel 
                { 
                    Id = 3, 
                    Numero = "003", 
                    Cliente = "Cliente d", 
                    Valor = 3000.00m, 
                    Vencimento = new DateTime(2024, 04, 01), 
                    Status = "Andamento" 
                }
            };

            return View(titulos);
        }
    }
}
