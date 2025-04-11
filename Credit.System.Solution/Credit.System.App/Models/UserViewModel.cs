namespace Credit.System.App.Models
{
    public class UserViewModel
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        
        public string Email { get; set; }
       
        public string Login { get; set; }

        public string Phone { get; set; }

        public bool isActive { get; set; }
    
    }
}
