using Platform.Repository;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Utils.Security;

namespace Platform.Transactional.Operations
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
                user.CompanyId = 1;

                user.Password = "POkN8Tt8TnnfTAzUERL/6w=="; //123
                
                
                //usar somente quando a funcionalidade de envio de email estiver pronta.

                //string firstPassword = Security.GeneratePassword();
                //user.Password = Security.Encrypt(firstPassword);
                
                _serviceSystemConnection.Add(user);
                _serviceSystemConnection.SaveChanges();
			
			}
			catch (Exception)
			{
				throw;
			}
        }

        public User GetUserByLoginAndPassword(string login, string password)
        {
            User user = new User();

            try
            {
                user = _serviceSystemConnection.User.Where(x => x.Login == login && x.Password == Security.Encrypt(password)).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

            return user;
        
        }

        public bool UserExistsAtCompany(string login, long companyId)
        {
            try
            {
                return _serviceSystemConnection.User
                    .Any(x => x.Login == login && x.CompanyId == companyId);
            }
            catch
            {
                return false; // Or rethrow depending on your requirements
            }
        }

    }
}
