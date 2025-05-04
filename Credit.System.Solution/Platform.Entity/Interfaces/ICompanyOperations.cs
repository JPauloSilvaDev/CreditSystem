using Platform.Entity.ServiceSystem;


namespace Platform.Entity.Interfaces
{
    public interface ICompanyOperations
    {
    
        public List<Company> GetAllCompanies();

        public Company GetCompanyById(long id);

        public void InsertCompany(Company company);
        
        public void UpdateCompany(Company company);
    }
}
