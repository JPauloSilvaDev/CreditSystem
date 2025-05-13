using System.ComponentModel;


namespace Platform.Entity.ServiceSystem
{
    public class Client : BaseEntity
    {
        public long ClientId { get; set; }
       
        public long CompanyId { get; set; }
        
        public string PrimaryName { get; set; }

        public string? SecondaryName { get; set; }
        
        public string Email { get; set; }

        [Description("CPF/CNPJ")]
        public string Document { get; set; }

        [Description("Telefone 1")]
        public string PrimaryPhone { get; set; }

        [Description("Telefone 2")]
        public string? SecondaryPhone { get;set; }
       
        [Description("Cep")]
        public string ZipCode { get; set; }

        [Description("Rua")]
        public string Street { get; set; }

        [Description("Número")]
        public short StreetNumber { get; set; }
        
        [Description("Observação")]
        public string? Observation { get; set; }

        [Description("Estado")]
        public string State { get; set; }

        [Description("Cidade")]
        public string City { get; set; }
        
        [Description("Bairro")]
        public string Neighborhood { get; set; }

    }
}
