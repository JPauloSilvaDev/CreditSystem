using System.Text.Json.Serialization;

namespace Platform.Entity.ServiceSystem
{
    public class User : BaseEntity
    {
        public long UserId { get; set; }
      
        public string Name { get; set; }
        
        public string Login { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
      
        public bool IsActive { get; set; }

        public long CompanyId { get; set; }
    }
}

