using Credit.System.App.Repository;
using DataBase.Operations.Interfaces;
using DataBase.Operations.Tables.ServiceSystem;
using Utils.Security;

namespace DataBase.Operations
{
    public class UserOperations : IUserOperations
    {
        private readonly DataBaseConnection _serviceSystemConnection;

        //modify this as static to always get the servicesystem db connection

        public UserOperations(DataBaseConnection serviceSystemConnection)
        {
            _serviceSystemConnection = serviceSystemConnection;
        }

        public void InsertUser(User user)
        {
			try
			{
				user.IsActive = true;
				user.CreationDate = DateTime.Now;
                user.Password = Security.Encrypt(user.Password);
                user.CreationDate = DateTime.Now;
                user.Login = user.Login;

                _serviceSystemConnection.Add(user);
                _serviceSystemConnection.SaveChanges();
			
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
