﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity UserRegistration(UserRegistrationModel registrationModel);
        public UserLoginModel UserLogin(UserLoginModel loginModel);
    }
}
