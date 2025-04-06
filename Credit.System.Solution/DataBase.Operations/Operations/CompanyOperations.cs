using Platform.Repository;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;

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
                List<Company> companies = _serviceSystemConnection.Company.ToList();
                return companies;
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
