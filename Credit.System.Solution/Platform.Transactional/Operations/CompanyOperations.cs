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


        public void UpdateCompany(Company company)
        {
            try
            {
                Company editCompany = GetCompanyById(company.CompanyId);

                editCompany.Document = company.Document;
                editCompany.PrimaryName = company.PrimaryName;
                editCompany.SecondaryName = company.SecondaryName;
                editCompany.PrimaryPhone = company.PrimaryPhone;
                editCompany.SecondaryPhone = company.SecondaryPhone;
                editCompany.Email = company.Email;
                editCompany.ZipCode = company.ZipCode;
                editCompany.AddressNumber = company.AddressNumber;
                editCompany.City = company.City;
                editCompany.State = company.State;
                editCompany.Observation = company.Observation;
                editCompany.Neighborhood = company.Neighborhood;
                editCompany.AddressNumber = company.AddressNumber;
                editCompany.Street = company.Street;
                editCompany.UpdateDate = DateTime.Now;
             
                _serviceSystemConnection.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}
