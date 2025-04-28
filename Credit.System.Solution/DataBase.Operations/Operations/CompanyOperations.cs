using Platform.Repository;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Microsoft.EntityFrameworkCore;

namespace Platform.Transactional.Operations
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
                return _serviceSystemConnection.Company.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        
        }

        public Company GetCompanyById(long id)
        {
            try
            {
               return _serviceSystemConnection.Company.Where(x => x.CompanyId == id).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

        }


        public void InsertCompany(Company company)
        {
            try
            {
                company.CreationDate = DateTime.Now;
                _serviceSystemConnection.Add(company);
                _serviceSystemConnection.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        
        }

    }
}
