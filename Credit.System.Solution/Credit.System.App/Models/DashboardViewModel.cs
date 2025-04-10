using System.Collections.Generic;

namespace Credit.System.App.Models
{
    public class DashboardViewModel
    {
        public List<string> Months { get; set; }
        public List<decimal> Sales { get; set; }
        public List<decimal> Expenses { get; set; }
        public List<CategoriaViewModel> Categories { get; set; }
        public List<ProdutoViewModel> Products { get; set; }
    }

    public class CategoriaViewModel
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    public class ProdutoViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
} 