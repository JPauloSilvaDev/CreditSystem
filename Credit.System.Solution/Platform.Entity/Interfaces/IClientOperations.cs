using Platform.Entity.ServiceSystem;

namespace Platform.Entity.Interfaces
{
    public interface IClientOperations
    {
        public Task InsertClientAsync(Client client);

        public Task UpdateClientAsync(Client client);
        
        public Task DeleteClientAsync(long clientId);
        
        public Task<List<Client>> GetClientsByCompanyIdAsync(long companyId);

        public Client GetClientById(long clientId);   
    
    }
}
