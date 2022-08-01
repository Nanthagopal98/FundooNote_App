using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity UserRegistration(UserRegistrationModel registrationModel);
        public string UserLogin(UserLoginModel loginModel);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string password, string confirmPassword);

    }
}
