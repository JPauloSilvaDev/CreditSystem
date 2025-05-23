﻿using Platform.Entity.ServiceSystem;

namespace Credit.System.App.Models
{
    public class RegisterUserModel
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public long CompanyId { get; set; }

        public List<UserViewModel> Users { get; set; }

        public List<Company> CompanyList { get; set; }
    }
}
