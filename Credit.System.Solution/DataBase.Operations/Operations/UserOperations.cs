using Platform.Repository;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Utils.Security;
using System.ComponentModel.Design;

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
                user = _serviceSystemConnection.User.Where(x => x.Login == login && x.Password == Security.Encrypt(password) && x.IsActive == true && x.DeletionDate == null).FirstOrDefault();
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
                    .Any(x => x.Login == login && x.CompanyId == companyId && x.DeletionDate == null);
            }
            catch
            {
                return false;
            }
        }

        public List<User> GetAllUsersByCompanyId(long companyId)
        {
            List<User> users = new List<User>();

            try
            {
                users = _serviceSystemConnection.User.Where(x => x.CompanyId == companyId && x.DeletionDate == null).ToList();
            }
            catch (Exception)
            {
                throw;
            }
           
            return users;
        }

        public void UpdateUser(User editedData)
        {
            try
            {
                User userToEdit = GetUserByid(editedData.UserId);

                userToEdit.Login = editedData.Login;
                userToEdit.Email = editedData.Email;
                userToEdit.Name = editedData.Name;
                userToEdit.Password = userToEdit.Password;
                userToEdit.UpdateDate = DateTime.Now;
                
                _serviceSystemConnection.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public User GetUserByid(long userId)
        {
            try
            {
                User user = _serviceSystemConnection.User.Where(x => x.UserId == userId).FirstOrDefault();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void DeleteUser(long userId)
        {
            User userToRemove = GetUserByid(userId);
            
            userToRemove.DeletionDate = DateTime.Now;
            userToRemove.IsActive = false;

            _serviceSystemConnection.SaveChanges();
        }

        public void BlockUserAccess(long userId)
        {
            try
            {
                User user = GetUserByid(userId);

                user.IsActive = false;

                _serviceSystemConnection.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void UnblockUserAccess(long userId)
        {
            try
            {
                User user = GetUserByid(userId);

                user.IsActive = true;
                
                _serviceSystemConnection.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
