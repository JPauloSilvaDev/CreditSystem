using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DataBase.Operations.Tables.ServiceSystem
{
    public class User : BaseFields
    {
        [Column("UserId")]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }
      
        public bool IsActive { get; set; }

        //public DateTime CreationDate { get; set; }

        //public DateTime? DeletionDate { get; set; }

        public long CompanyId { get; set; }
    }
}

