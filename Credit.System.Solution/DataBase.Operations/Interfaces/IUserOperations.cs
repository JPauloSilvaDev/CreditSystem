using DataBase.Operations.Tables.ServiceSystem;

namespace DataBase.Operations.Interfaces
{
    public interface IUserOperations
    {
        public void InsertUser(User user);
        
        public User GetUserByLoginAndPassword(string user, string password);
    }
}
