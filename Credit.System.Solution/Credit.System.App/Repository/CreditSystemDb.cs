using System.Collections.Generic;
using Credit.System.App.TableModels.ServiceSystem;
using Microsoft.EntityFrameworkCore;

namespace Credit.System.App.Repository
{
    public class DataBaseConnection : DbContext
    {
        public DataBaseConnection(DbContextOptions<DataBaseConnection> options) : base(options) { }
        public DbSet<User> User { get; set; }
    }

}