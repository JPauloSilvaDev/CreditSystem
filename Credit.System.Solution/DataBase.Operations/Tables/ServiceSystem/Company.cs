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
    
    }
}
