using Platform.Entity.ServiceSystem;

namespace Platform.Entity.Interfaces
{
    public interface IUserOperations
    {
        public Task InsertUserAsync(User user);
        
        public Task<User> GetUserByLoginAndPasswordAsync(string user, string password);

        public Task<bool> UserExistsAtCompanyAsync(string login, long companyId);

        public Task<List<User>> GetAllUsersByCompanyIdAsync(long companyId);

        public Task UpdateUserAsync(User user);

        public Task DeleteUserAsync(long userId);

        public Task<User> GetUserByIdAsync(long userId);
        
        public Task BlockUserAccessAsync(long userId);
        
        public Task UnblockUserAccessAsync(long userId);

    }
}
