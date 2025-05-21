using Microsoft.EntityFrameworkCore;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Platform.Repository;
using System.Text.Json.Nodes;

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
            try
            {
                client.CreationDate = DateTime.Now;
                await _serviceSystemConnection.AddAsync(client);
                await _serviceSystemConnection.SaveChangesAsync();
            }

            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpdateClientAsync(Client client)
        {
            try
            {
                Client clientToEdit = _serviceSystemConnection.Client.Find(client.ClientId);

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
            catch (Exception)
            {
                throw;
            }

        }

        public async Task DeleteClientAsync(long clientId)
        {
            try
            {
                Client client = _serviceSystemConnection.Client.Where(x => x.ClientId == clientId && x.DeletionDate == null).FirstOrDefault();//get client by clientid.

                client.DeletionDate = DateTime.Now;
                client.UpdateDate = DateTime.Now;

               await _serviceSystemConnection.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Client>> GetClientsByCompanyIdAsync(long companyId)
        {
            try
            {
                return await _serviceSystemConnection.Client.Where(x => x.CompanyId == companyId && x.DeletionDate == null).Take(1000).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Client GetClientById(long clientId)
        {
            try
            {
                return _serviceSystemConnection.Client.Where(x => x.ClientId == clientId && x.DeletionDate == null).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
