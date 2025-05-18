using Platform.Entity.ServiceSystem;

namespace Platform.Entity.Interfaces
{
    public interface IDebtorOperations
    {
        public void InsertDebtor(Debtor debtor);
        public void UpdateDebtor(Debtor debtor);
        public void DeleteDebtor(long debtorId);
        public List<Debtor> GetDebtorsByCompanyId(long companyId);
        
    }
}
