using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using Platform.Repository;
using Platform.Utils;
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

        public async Task InsertUserAsync(User user)
        {
            bool userExists = await UserExistsAtCompanyAsync(user.Login, user.CompanyId);

            if (userExists)
                throw new CSException(string.Format(CustomExceptionMessage.UserMessage0000, user.Login));

            user.IsActive = true;
            user.CreationDate = DateTime.Now;
            user.Login = user.Login;
            user.Password = "POkN8Tt8TnnfTAzUERL/6w=="; //123

            //usar somente quando a funcionalidade de envio de email estiver pronta.

            //string firstPassword = Security.GeneratePassword();
            //user.Password = Security.Encrypt(firstPassword);

            await _serviceSystemConnection.AddAsync(user);
            await _serviceSystemConnection.SaveChangesAsync();

        }

        public async Task<User> GetUserByLoginAndPasswordAsync(string login, string password)
        {

            return await _serviceSystemConnection.User.FirstOrDefaultAsync(x => x.Login == login && x.Password == Security.Encrypt(password) && x.IsActive == true &&
            x.DeletionDate == null);

        }

        public async Task<bool> UserExistsAtCompanyAsync(string login, long companyId)
        {

            return _serviceSystemConnection.User
                .Any(x => x.Login == login && x.CompanyId == companyId &&
                x.DeletionDate == null);

        }

        public async Task<List<User>> GetAllUsersByCompanyIdAsync(long companyId)
        {

            try
            {
                return await _serviceSystemConnection.User.Where(x => x.CompanyId == companyId &&
                x.DeletionDate == null).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task UpdateUserAsync(User editedData)
        {
            try
            {
                User userToEdit = await GetUserByIdAsync(editedData.UserId);

                userToEdit.Login = editedData.Login;
                userToEdit.Email = editedData.Email;
                userToEdit.Name = editedData.Name;
                userToEdit.Password = userToEdit.Password;
                userToEdit.UpdateDate = DateTime.Now;

                _serviceSystemConnection.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<User> GetUserByIdAsync(long userId)
        {
            try
            {
                return await  _serviceSystemConnection.User.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task DeleteUserAsync(long userId)
        {
            User userToRemove = await GetUserByIdAsync(userId);

            userToRemove.DeletionDate = DateTime.Now;
            userToRemove.IsActive = false;

            _serviceSystemConnection.SaveChanges();
        }

        public async Task BlockUserAccessAsync(long userId)
        {
            try
            {
                User user = await GetUserByIdAsync(userId);

                user.IsActive = false;

                _serviceSystemConnection.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task UnblockUserAccessAsync(long userId)
        {
            try
            {
                User user = await GetUserByIdAsync(userId);

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
