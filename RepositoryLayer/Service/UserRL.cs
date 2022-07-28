using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public UserEntity UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = registrationModel.FirstName;
                userEntity.LastName = registrationModel.LastName;   
                userEntity.Email = registrationModel.Email;
                userEntity.Password = registrationModel.Password;

                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if(result != 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
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
                var result = this.fundooContext.UserTable.Where(data => data.Email == loginModel.Email &&
                data.Password == loginModel.Password).FirstOrDefault();
                if(result != null)
                {
                    UserLoginModel verifiedLogin = new UserLoginModel();
                    verifiedLogin.Email = result.Email;
                    verifiedLogin.Password = result.Password;
                    return verifiedLogin;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
