using Credit.System.App.Repository;
using DataBase.Operations.Interfaces;
using DataBase.Operations.Tables.ServiceSystem;
using Utils.Security;

namespace DataBase.Operations
{
    public class UserOperations : IUserOperations
    {
        private readonly ServiceSystemConnection _serviceSystemConnection;

        public UserOperations(ServiceSystemConnection serviceSystemConnection)
        {
            _serviceSystemConnection = serviceSystemConnection;
        }

        public void InsertUser(User user)
        {
			try
			{
				user.IsActive = true;
				user.CreationDate = DateTime.Now;
                user.Login = user.Login;

                string firstPassword = Security.GeneratePassword();
                
                user.Password = Security.Encrypt(firstPassword);
                
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
