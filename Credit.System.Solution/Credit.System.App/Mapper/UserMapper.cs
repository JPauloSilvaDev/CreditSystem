using Credit.System.App.Models;
using Platform.Entity.ServiceSystem;

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
                    CompanyId = userModel.CompanyId,
                };
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static List<UserViewModel> MapUserListToUserViewModelList(List<User> userModel)
        {
            try
            {
                List<UserViewModel> userViewModelList = new List<UserViewModel>();

                foreach (User user in userModel)
                {
                    UserViewModel userViewModel = new UserViewModel()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        Login = user.Login,
                        Name = user.Name,
                        isActive = user.IsActive,
                    };

                    userViewModelList.Add(userViewModel);
                }

                return userViewModelList;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static User MapUserViewModelToUser(UserViewModel userViewModel)
        {
            try
            {
                User user = new User()
                {
                    UserId = userViewModel.UserId,
                    Email = userViewModel.Email,
                    Login = userViewModel.Login,
                    Name = userViewModel.Name,
                    IsActive = userViewModel.isActive
                };

                return user;
            }
            catch (Exception)
            {
                throw;
            }

        }








    }
}
