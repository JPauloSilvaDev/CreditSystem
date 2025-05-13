using System.ComponentModel;

namespace Platform.Entity.ServiceSystem
{
    public class Company: BaseEntity
    {
        public long CompanyId { get; set; }
        
        [Description("Razão Social")]
        public string PrimaryName { get; set; }
       
        [Description("Nome Fantasia")]
        public string? SecondaryName { get; set; }
        
        [Description("CNPJ")]
        public string Document { get; set; }
        
        [Description("Telefone 1")]
        public string? PrimaryPhone { get; set; }
        
        [Description("Telefone 2")]
        public string? SecondaryPhone { get; set; }

        public string? Email { get; set; }
       
        public string? ZipCode { get; set; }
        
        [Description("Número da Rua")]
        public short? AddressNumber { get; set; }
        
        public string? City { get; set; }
       
        public string? State { get; set; }
       
        public string? Observation { get; set; }
        
        [Description("Bairro")]
        public string? Neighborhood { get; set; }
        
        [Description("Cobrança Terceirizada")]
        public bool IsThirdPartyCollection { get; set; }

    }
}
