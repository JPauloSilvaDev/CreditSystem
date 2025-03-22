
namespace Credit.System.App.TableModels.ServiceSystem
{
    public class User
    {
       
        public int UserId { get; set; }

   
        public string Login { get; set; }

 
        public string Password { get; set; }

      
        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }


        public DateTime? DeletionDate { get; set; }
    }
}

