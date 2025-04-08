using System.Diagnostics;
using Credit.System.App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Credit.System.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var userLogged = HttpContext.Session.GetString("UserLogged");

            if (userLogged == null)
                return RedirectToAction("Index", "User");

            // Aqui você pode buscar os dados reais do seu banco de dados
            var dashboardData = new DashboardViewModel
            {
                Meses = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                Vendas = new List<decimal> { 1000, 1500, 2000, 1800, 2200, 2500 },
                Despesas = new List<decimal> { 800, 1200, 1500, 1400, 1800, 2000 },
                Categorias = new List<CategoriaViewModel>
                {
                    new CategoriaViewModel { Nome = "Categoria A", Valor = 30 },
                    new CategoriaViewModel { Nome = "Categoria B", Valor = 25 },
                    new CategoriaViewModel { Nome = "Categoria C", Valor = 20 },
                    new CategoriaViewModel { Nome = "Categoria D", Valor = 15 },
                    new CategoriaViewModel { Nome = "Categoria E", Valor = 10 }
                },
                Produtos = new List<ProdutoViewModel>
                {
                    new ProdutoViewModel { Nome = "Produto 1", Quantidade = 500 },
                    new ProdutoViewModel { Nome = "Produto 2", Quantidade = 300 },
                    new ProdutoViewModel { Nome = "Produto 3", Quantidade = 400 },
                    new ProdutoViewModel { Nome = "Produto 4", Quantidade = 200 },
                    new ProdutoViewModel { Nome = "Produto 5", Quantidade = 600 }
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






