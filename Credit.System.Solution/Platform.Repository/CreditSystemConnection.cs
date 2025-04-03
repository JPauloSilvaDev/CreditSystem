using Microsoft.EntityFrameworkCore;

namespace Platform.Repository
{
    public class CreditSystemConnection : DbContext
    {
        public CreditSystemConnection(DbContextOptions<CreditSystemConnection> options)
                  : base(options) { }


        // Insira aqui as tabelas do banco dessa forma: 
        //        public DbSet<ObjetoTabela> Nome_Tabela { get; set; }

    }
}
