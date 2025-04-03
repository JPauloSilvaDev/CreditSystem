using DataBase.Operations.Tables.ServiceSystem;
using Microsoft.EntityFrameworkCore;

namespace Credit.System.App.Repository
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