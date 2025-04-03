using Platform.Entity.ServiceSystem;


namespace Platform.Entity.Interfaces
{
    public interface ICompanyOperations
    {
    
        public List<Company> GetAllCompanies();

        public void InsertCompany(Company company);
    }
}
