using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
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
    }
}
