using Microsoft.EntityFrameworkCore;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Platform.Repository;

namespace Platform.Transactional.Operations
{
    public class ClientOperations : IClientOperations
    {
        private readonly ServiceSystemConnection _serviceSystemConnection;

        public ClientOperations(ServiceSystemConnection serviceSystemConnection)
        {
            _serviceSystemConnection = serviceSystemConnection;
        }

        public async Task InsertClientAsync(Client client)
        {
            client.CreationDate = DateTime.Now;
           
            await _serviceSystemConnection.AddAsync(client);
            await _serviceSystemConnection.SaveChangesAsync();

        }
        public async Task UpdateClientAsync(Client client)
        {
            Client clientToEdit = await GetClientByIdAsync(client.ClientId);

            clientToEdit.PrimaryName = client.PrimaryName;
            clientToEdit.SecondaryName = client.SecondaryName;
            clientToEdit.Email = client.Email;
            clientToEdit.Document = client.Document;
            clientToEdit.PrimaryPhone = client.PrimaryPhone;
            clientToEdit.SecondaryPhone = client.SecondaryPhone;
            clientToEdit.ZipCode = client.ZipCode;
            clientToEdit.Street = client.Street;
            clientToEdit.StreetNumber = client.StreetNumber;
            clientToEdit.Observation = client.Observation;
            clientToEdit.State = client.State;
            clientToEdit.City = client.City;
            clientToEdit.Neighborhood = client.Neighborhood;

            await _serviceSystemConnection.SaveChangesAsync();

        }

        public async Task DeleteClientAsync(long clientId)
        {
            Client client = await GetClientByIdAsync(clientId);

            client.DeletionDate = DateTime.Now;
            client.UpdateDate = DateTime.Now;

            await _serviceSystemConnection.SaveChangesAsync();
        }

        public async Task<List<Client>> GetClientsByCompanyIdAsync(long companyId)
        {
            return await _serviceSystemConnection.Client.Where(x => x.CompanyId == companyId && x.DeletionDate == null).Take(1000).ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(long clientId)
        {
            return await _serviceSystemConnection.Client.Where(x => x.ClientId == clientId && x.DeletionDate == null).FirstOrDefaultAsync();
        }

        public async Task<List<Client>> GetClientsByDocumentAndCompanyIdAsync(string document, long companyId)
        {
            return await _serviceSystemConnection.Client.Where(x => x.Document.Contains(document) && x.CompanyId == companyId && x.DeletionDate == null).ToListAsync();
        }

        public async Task<List<Client>> GetClientsByNameAndCompanyId(string name, long companyId)
        {
            return await _serviceSystemConnection.Client.Where(x => (x.PrimaryName.Contains(name) || x.SecondaryName.Contains(name)) && x.CompanyId == companyId && x.DeletionDate == null).ToListAsync();
        }

    }
}
