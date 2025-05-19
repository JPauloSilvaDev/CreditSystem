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
            try
            {
                debtor.CreationDate = DateTime.Now;
                _serviceSystemConnection.AddAsync(debtor);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateDebtor(Debtor debtor)
        {

        }
        public void DeleteDebtor(long debtorId)
        {

        }


        public List<Debtor> GetDebtorsByCompanyId(long companyId)
        {
            try
            {
                return _serviceSystemConnection.Debtor.Where(x => x.CompanyId == companyId).ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
