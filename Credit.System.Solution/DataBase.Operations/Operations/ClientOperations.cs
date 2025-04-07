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


    }
}
