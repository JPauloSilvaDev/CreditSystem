
namespace Credit.System.App.TableModels.ServiceSystem
{
    public class User
    {
        // Unique identifier for each user (mapped to UserId)
        public int UserId { get; set; }

        // User's login name (mapped to Login)
        public string Login { get; set; }

        // User's password (mapped to Password)
        public string Password { get; set; }

        // Indicates whether the user account is active (mapped to IsActive)
        public bool IsActive { get; set; }

        // Date and time when the user account was created (mapped to CreationDate)
        public DateTime CreationDate { get; set; }

        // Date and time when the user account was deleted (mapped to DeletionDate, nullable)
        public DateTime? DeletionDate { get; set; }
    }
}

