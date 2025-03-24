
using System.ComponentModel.DataAnnotations;

namespace DataBase.Operations.Tables.ServiceSystem
{
    public class User
    {
       
        public int UserId { get; set; }
            
        public string Login { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }
      
        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? DeletionDate { get; set; }

        public long CompanyId { get; set; }
    }
}

