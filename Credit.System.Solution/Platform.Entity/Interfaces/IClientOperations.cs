using Platform.Entity.ServiceSystem;

namespace Platform.Entity.Interfaces
{
    public interface IClientOperations
    {
        public Task InsertClientAsync(Client client);

        public Task UpdateClientAsync(Client client);
        
        public Task DeleteClientAsync(long clientId);
        
        public Task<List<Client>> GetClientsByCompanyIdAsync(long companyId);
        
        public Task<List<Client>> GetClientsByDocumentAndCompanyIdAsync(string document, long companyId);

        public Task<Client> GetClientByIdAsync(long clientId);
        public Task<List<Client>> GetClientsByNameAndCompanyId(string name, long companyId);
    }
}
