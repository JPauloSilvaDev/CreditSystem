using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Credit.System.App.Repository;
using DataBase.Operations.Interfaces;
using DataBase.Operations.Tables.ServiceSystem;

namespace DataBase.Operations
{
    public class CompanyOperations : ICompanyOperations
    {

        private readonly ServiceSystemConnection _serviceSystemConnection;

        public CompanyOperations(ServiceSystemConnection serviceSystemConnection)
        {
            _serviceSystemConnection = serviceSystemConnection;
        }


        public List<Company> GetAllCompanies() {

            try
            {
                List<Company> companies = _serviceSystemConnection.Company.ToList();
                return companies;
            }
            catch (Exception)
            {
                throw;
            }
        
        }

    }
}
