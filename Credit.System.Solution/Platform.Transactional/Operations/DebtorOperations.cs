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

        public void InsertDebtor(Debtor debtor)
        {

        }


        public List<Debtor> GetDebtorsByCompanyId(long companyId)
        {
            try
            {
                return _serviceSystemConnection.Debtor.Where(x=> x.CompanyId == companyId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        
        }


    }
}
