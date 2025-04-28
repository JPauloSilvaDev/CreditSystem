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

        public void InsertClient(Client client)
        {
            try
            {
                client.CreationDate = DateTime.Now;
                _serviceSystemConnection.Add(client);
                _serviceSystemConnection.SaveChanges();
            }

            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateClient(Client client)
        {
            try
            {
                _serviceSystemConnection.Client.Update(client);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void DeleteClient(Client client)
        {
            try
            {

                //using (var connection = new SqlConnection(_connectionString))
                //{
                //    return connection.QueryFirstOrDefault<Client>(
                //        "SELECT * FROM Clients WHERE ClientId = @ClientId", new { ClientId = clientId });
                //}



                //Client clientToEdit = _serviceSystemConnection.Client.Where(x => x.ClientId == client.ClientId && x.DeletionDate == null).FirstOrDefault();//get client by clientid.

                //clientToEdit.DeletionDate = DateTime.Now;
                //clientToEdit.UpdateDate = DateTime.Now;

                _serviceSystemConnection.Client.Update(client);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Client> GetClientsByCompanyId(long companyId)
        {
            try
            {
                return _serviceSystemConnection.Client.Where(x => x.CompanyId == companyId && x.DeletionDate == null).ToList();
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
