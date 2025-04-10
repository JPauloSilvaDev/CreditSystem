using System.Diagnostics;
using Credit.System.App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Credit.System.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var userLogged = HttpContext.Session.GetString("UserLogged");

            if (userLogged == null)
                return RedirectToAction("Index", "User");


            UserSessionModel userLoggedModel = JsonConvert.DeserializeObject<UserSessionModel>(HttpContext.Session.GetString("UserLogged"));

            ViewData["CompanyId"] = userLoggedModel.CompanyId;

            // Aqui você pode buscar os dados reais do seu banco de dados
            var dashboardData = new DashboardViewModel
            {
                Months = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                Sales = new List<decimal> { 1000, 1500, 2000, 1800, 2200, 2500 },
                Expenses = new List<decimal> { 800, 1200, 1500, 1400, 1800, 2000 },
                Categories = new List<CategoriaViewModel>
                {
                    new CategoriaViewModel { Name = "Categoria A", Value = 30 },
                    new CategoriaViewModel { Name = "Categoria B", Value = 25 },
                    new CategoriaViewModel {Name = "Categoria C", Value = 20},
                    new CategoriaViewModel {Name = "Categoria D", Value = 15},
                    new CategoriaViewModel { Name = "Categoria E", Value = 10 }
                },
                Products = new List<ProdutoViewModel>
                {
                    new ProdutoViewModel { Name = "Produto 1", Quantity = 500 },
                    new ProdutoViewModel { Name = "Produto 2", Quantity = 300 },
                    new ProdutoViewModel { Name = "Produto 3", Quantity = 400 },
                    new ProdutoViewModel { Name = "Produto 4", Quantity = 200 },
                    new ProdutoViewModel { Name = "Produto 5", Quantity = 600 }
                }
            };

            return View(dashboardData);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

    }
}






