using System;
using static Platform.Entity.Enums.Enums;

namespace Credit.System.App.Models
{
    public class BillViewModel
    {
        public int BillId { get; set; }
        public string Number { get; set; }
        public string Client { get; set; }
        public decimal Value { get; set; }
        public DateTime DueDate { get; set; }
        public BillStatus Status { get; set; }
    }
} 