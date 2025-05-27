using Platform.Entity.ServiceSystem;

namespace Platform.Entity.Interfaces
{
    public interface IDebtorOperations
    {
        public Task InsertDebtorAsync(Debtor debtor);
        public Task UpdateDebtorAsync(Debtor debtor);
        public Task DeleteDebtorAsync(long debtorId);
        public Task<Debtor> GetDebtorByIdAsync(long debtorId);
        public Task<List<Debtor>> GetDebtorsByCompanyIdAsync(long companyId);
        public Task<List<Debtor>> GetDebtorsByClientIdAndCompanyId(long  clientId, long companyId);
        public Task<List<Debtor>> GetDebtorByDocumentAndCompanyId(string document, long companyId);
        public Task<List<Debtor>> GetDebtorsByStateAndCompanyId(string state, long companyId);
        public Task<List<Debtor>> GetDebtorsByNameAndCompanyId(string state, long companyId);
        
    }
}
