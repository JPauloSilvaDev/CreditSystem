using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Operations.Tables.ServiceSystem;

namespace DataBase.Operations.Interfaces
{
    public interface ICompanyOperations
    {
    
        public List<Company> GetAllCompanies();
    
    }
}
