using Platform.Entity.ServiceSystem;

namespace Platform.Entity.Interfaces
{
    public interface IClientOperations
    {
        public void InsertClient(Client client);

        public void UpdateClient(Client client);
        
        public void DeleteClient(long clientId);

        public List<Client> GetClientsByCompanyId(long companyId);

        public Client GetClientById(long clientId);   
    
    }
}
