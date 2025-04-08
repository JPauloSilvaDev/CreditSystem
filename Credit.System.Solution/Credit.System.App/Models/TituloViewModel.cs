using System;

namespace Credit.System.App.Models
{
    public class TituloViewModel
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Cliente { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public string Status { get; set; }
    }
} 