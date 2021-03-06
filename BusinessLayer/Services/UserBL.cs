using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL; // readonly can only be assigned a value from within the constructor(s) of a class.
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public string ForgetPassword(string email)
        {
            try
            {
                return userRL.ForgetPassword(email);
            }
            catch (Exception)
            {

                throw;
            }
        }
            public User Registration(UserRegModel userRegModel)
            {
                try
                {
                    return userRL.Registration(userRegModel);
                }
                catch (Exception)
                {
                    throw;
                }


            }

            public LoginResponseModel UserLogin(UserLoginModel userLog)
            {
                try
                {
                    return this.userRL.UserLogin(userLog);
                }
                catch (Exception)
                {
                    throw;
                }
            }
           public bool ResetPassword(string email, string password, string confirmPassword)
           {
               try
               {
                 return this.userRL.ResetPassword(email, password, confirmPassword);
               }
               catch (Exception)
               {

                throw;
               }
           }




    }
}