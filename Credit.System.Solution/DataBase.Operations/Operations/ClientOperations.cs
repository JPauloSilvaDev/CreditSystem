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

        public void InsertClient(Client client)
        {
			try
			{
				client.CreationDate = DateTime.Now;
                _serviceSystemConnection.Add(client);
			}
			catch (Exception)
			{
				throw;
			}
        }


    }
}
