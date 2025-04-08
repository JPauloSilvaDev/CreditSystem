using System.Collections.Generic;

namespace Credit.System.App.Models
{
    public class DashboardViewModel
    {
        public List<string> Meses { get; set; }
        public List<decimal> Vendas { get; set; }
        public List<decimal> Despesas { get; set; }
        public List<CategoriaViewModel> Categorias { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; }
    }

    public class CategoriaViewModel
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }

    public class ProdutoViewModel
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
    }
} 