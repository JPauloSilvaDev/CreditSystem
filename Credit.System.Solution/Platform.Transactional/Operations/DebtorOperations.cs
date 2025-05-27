using Microsoft.EntityFrameworkCore;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Platform.Repository;

namespace Platform.Transactional.Operations
{
    public class DebtorOperations : IDebtorOperations
    {
        private readonly ServiceSystemConnection _serviceSystemConnection;

        public DebtorOperations(ServiceSystemConnection serviceSystemConnection)
        {
            _serviceSystemConnection = serviceSystemConnection;
        }

        public async Task InsertDebtorAsync(Debtor debtor)
        {
            debtor.CreationDate = DateTime.Now;
            
            await _serviceSystemConnection.AddAsync(debtor);
            await _serviceSystemConnection.SaveChangesAsync();
        }
        public async Task UpdateDebtorAsync(Debtor debtorData)
        {
            Debtor debtor = await GetDebtorByIdAsync(debtorData.DebtorId);

            debtor.DeletionDate = DateTime.Now;
            debtor.UpdateDate = DateTime.Now;

            await _serviceSystemConnection.SaveChangesAsync();
        }
        public async Task DeleteDebtorAsync(long debtorId)
        {
            Debtor debtor = await GetDebtorByIdAsync(debtorId);

            debtor.DeletionDate = DateTime.Now;
            debtor.UpdateDate = DateTime.Now;

            await _serviceSystemConnection.SaveChangesAsync();
        }
        public async Task<Debtor> GetDebtorByIdAsync(long debtorId)
        {
            return await _serviceSystemConnection.Debtor.FirstOrDefaultAsync(x => x.DebtorId == debtorId);
        }

        public async Task<List<Debtor>> GetDebtorsByCompanyIdAsync(long companyId)
        {
            return await _serviceSystemConnection.Debtor.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<List<Debtor>> GetDebtorsByClientIdAndCompanyId(long clientId, long companyId)
        {
            return await _serviceSystemConnection.Debtor.Where(x => x.ClientId == clientId && x.CompanyId == companyId).ToListAsync();
        }

        public async Task<List<Debtor>> GetDebtorByDocumentAndCompanyId(string document, long companyId)
        {
            return await _serviceSystemConnection.Debtor.Where(x=>x.Document.Contains(document) && x.CompanyId == companyId && x.DeletionDate == null).ToListAsync();
        }

        public async Task<List<Debtor>> GetDebtorsByStateAndCompanyId(string state, long companyId)
        {
            return await _serviceSystemConnection.Debtor.Where(x => x.State == state && x.CompanyId == companyId && x.DeletionDate == null).ToListAsync();
        }
        public async Task<List<Debtor>> GetDebtorsByNameAndCompanyId(string name, long companyId)
        {
            return await _serviceSystemConnection.Debtor.Where(x => (x.PrimaryName.Contains(name) || x.SecondaryName.Contains(name)) && x.CompanyId == companyId && x.DeletionDate == null).ToListAsync();
        }

    }
}
