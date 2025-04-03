using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.Entity.ServiceSystem
{
    public class Company: BaseEntity
    {
        [Column("CompanyId")]
        public long CompanyId { get; set; }
        
        [Description("Razão Social")]
        public string PrimaryName { get; set; }
       
        [Description("Nome Fantasia")]
        public string SecondaryName { get; set; }
        
        [Description("CNPJ")]
        public string Document { get; set; }
        
        [Description("Telefone 1")]
        public string PrimaryPhone { get; set; }
        
        [Description("Telefone 2")]
        public string? SecondaryPhone { get; set; }

        public string Email { get; set; }
       
        public string ZipCode { get; set; }
       
        public short AddressNumber { get; set; }
       
        public string City { get; set; }
       
        public string State { get; set; }
       
        public string? Observation { get; set; }
        
        public string Neighborhood { get; set; }


    }
}
