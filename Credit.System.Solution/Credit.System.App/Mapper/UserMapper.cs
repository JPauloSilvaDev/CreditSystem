using Credit.System.App.Models;
using DataBase.Operations.Tables.ServiceSystem;

namespace Credit.System.App.Mapper
{
    public static class UserMapper
    {
        public static User MapRegisterUserModelToUser(RegisterUserModel userModel)
        {
            try
            {
                return new User 
                {
                    Email = userModel.Email,
                    Login = userModel.Login,
                    Name = userModel.Name,
                    CompanyId = userModel.CompanyId.Value,
                };
            }
            catch (Exception)
            {
                throw;
            }

        }
    
    
    }
}
