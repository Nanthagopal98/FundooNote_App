using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration config;
        private readonly FundooContext fundooContext;
        public UserRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this.config = config;
        }
        public UserEntity UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = registrationModel.FirstName;
                userEntity.LastName = registrationModel.LastName;   
                userEntity.Email = registrationModel.Email;
                userEntity.Password = CommonMethods.ConvertToEncrypt(registrationModel.Password);

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
            catch (Exception )
            {

                throw;
            }
        }
        public string UserLogin(UserLoginModel loginModel)
        {
            try
            {
                var result = this.fundooContext.UserTable.Where(data => data.Email == loginModel.Email).FirstOrDefault();
                if(result != null && CommonMethods.ConvertToDecrypt(result.Password) == loginModel.Password)
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw ;
            }
        }
        public string GenerateSecurityToken(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config[("JWT:Key")]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        public string ForgetPassword(string email)
        {
            try
            {
                var CheckEmail = fundooContext.UserTable.FirstOrDefault(e => e.Email == email);
                if(CheckEmail != null)
                {
                    var Token = GenerateSecurityToken(CheckEmail.Email, CheckEmail.UserId);
                    MSMQModel msmqModel = new MSMQModel();
                    msmqModel.sendDatatoQueue(Token);
                    return Token.ToString();
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
        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            if (password.Equals(confirmPassword))
            {
                var verifyEmail = fundooContext.UserTable.FirstOrDefault(e => e.Email == email);
                verifyEmail.Password = password;

                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
