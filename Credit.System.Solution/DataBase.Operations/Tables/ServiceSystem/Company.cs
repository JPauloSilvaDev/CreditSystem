using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Operations.Tables.ServiceSystem
{
    public class Company: BaseFields
    {
        [Column("CompanyId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CompanyId { get; set; }
        [Description("Razão Social")]
        public string PrimaryName { get; set; }
        [Description("Nome Fantasia")]
        public string SecondName { get; set; }
        public string Document { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondPhone { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public short AddressNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Observation { get; set; }
        public string Neighborhood { get; set; }


    }
}
