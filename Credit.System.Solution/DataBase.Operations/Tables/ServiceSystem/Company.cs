namespace DataBase.Operations.Tables.ServiceSystem
{
    public class Company
    {
    
        public long CompanyId { get; set; }
        
        public string Name { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime CreationDate { get; set; }
 
        public DateTime? DeletionDate { get; set; }

        public string Email { get; set; }
    
        public string Address { get; set; }

        public short AddressNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
    }
}
