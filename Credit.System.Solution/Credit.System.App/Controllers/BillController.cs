using Credit.System.App.Models;
using Microsoft.AspNetCore.Mvc;
using static Platform.Entity.Enums.BillEnums;

namespace Credit.System.App.Controllers
{
    public class BillController : Controller
    {
        public IActionResult Index()
        {
            // Simulando dados do banco de dados, será alterado para um filtro com parametrização futuramente quando for adicionar as funcionalidades.
           
            var bills = new List<BillViewModel>
            {
                new BillViewModel
                { 
                    BillId = 1, 
                    Number = "001", 
                    Client = "Cliente A", 
                    Value = 1000.00m, 
                    DueDate = new DateTime(2024, 03, 01), 
                    Status  = BillStatus.Pending
                },
                new BillViewModel
                { 
                    BillId = 2, 
                    Number = "002", 
                    Client = "Cliente B", 
                    Value = 2000.00m, 
                    DueDate = new DateTime(2024, 03, 15), 
                    Status = BillStatus.Settled
                },
                new BillViewModel
                { 
                    BillId = 3, 
                    Number = "003", 
                    Client = "Cliente C", 
                    Value = 3000.00m, 
                    DueDate = new DateTime(2024, 04, 01), 
                    Status = BillStatus.Pending
                },
                new BillViewModel
                { 
                    BillId = 3, 
                    Number = "003", 
                    Client = "Cliente d", 
                    Value = 3000.00m, 
                    DueDate = new DateTime(2024, 04, 01), 
                    Status = BillStatus.Ongoing
                }
            };

            return View(bills);
        }
    }
}
