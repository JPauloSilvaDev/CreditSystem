using Platform.Entity.ServiceSystem;

namespace Platform.Entity.Interfaces
{
    public interface IUserOperations
    {
        public void InsertUser(User user);
        
        public User GetUserByLoginAndPassword(string user, string password);

        public bool UserExistsAtCompany(string login, long companyId);

        public List<User> GetAllUsersByCompanyId(long companyId);

        public void UpdateUser(User user);

        public void DeleteUser(long userId);

        public User GetUserByid(long userId);
        public void BlockUserAccess(long userId);
        public void UnblockUserAccess(long userId);

    }
}
