using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

        public UserEntity UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                return iuserRL.UserRegistration(registrationModel);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public UserLoginModel UserLogin(UserLoginModel loginModel)
        {
            try
            {
                return iuserRL.UserLogin(loginModel);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
