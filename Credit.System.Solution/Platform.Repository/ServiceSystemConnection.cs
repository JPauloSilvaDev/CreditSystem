using Microsoft.EntityFrameworkCore;
using Platform.Entity.ServiceSystem;

namespace Platform.Repository
{
    public class ServiceSystemConnection : DbContext
    {
        public ServiceSystemConnection(DbContextOptions<ServiceSystemConnection> options)
            : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Client> Client { get; set; }


    }

}