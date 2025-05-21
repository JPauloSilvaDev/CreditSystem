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
        }
        public async Task UpdateDebtorAsync(Debtor debtor)
        {
           
        }
        public async Task DeleteDebtorAsync(long debtorId)
        {
            
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


    }
}
