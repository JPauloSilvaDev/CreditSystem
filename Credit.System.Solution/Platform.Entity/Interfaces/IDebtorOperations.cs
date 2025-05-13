using Platform.Entity.ServiceSystem;

namespace Platform.Entity.Interfaces
{
    public interface IDebtorOperations
    {
        public void InsertDebtor(Debtor debtor);
        public List<Debtor> GetDebtorsByCompanyId(long companyId);
        
    }
}
