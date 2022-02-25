using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public User Registration(UserRegModel userRegModel);


        public LoginResponseModel UserLogin(UserLoginModel userLog);
        public string ForgetPassword(string email);

        public bool ResetPassword(string email, string password, string confirmPassword);
    }

}
