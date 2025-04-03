using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Operations.Tables.ServiceSystem
{
    public class Client : BaseFields
    {
        public long ClientId { get; set; }
       
        public long CompanyId { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }

        [Description("CPF/CNPJ")]
        public string Document { get; set; }

        [Description("Telefone 1")]
        public string PrimaryPhone { get; set; }

        [Description("Telefone 2")]
        public string? SecondPhone { get;set; }

        public string? Zipcode { get; set; }
        
        public string? Street { get; set; }

        [Description("Número")]
        public string? StreetNumber { get; set; }
        
        [Description("Observação")]
        public string? Observation { get; set; }

        [Description("Estado")]
        public string? State { get; set; }

        [Description("Cidade")]
        public string? City { get; set; }
        
        [Description("Bairro")]
        public string? Neighborhood { get; set; }

    }
}
