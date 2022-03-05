
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext; //context class is used to query or save data to the database.
        IConfiguration _Appsettings;  //IConfiguration interface is used to read Settings and Connection Strings from AppSettings.

        public UserRL(FundooContext fundooContext, IConfiguration Appsettings)
        {
            this.fundooContext = fundooContext;
            _Appsettings = Appsettings;
        }

        
        public User Registration(UserRegModel userRegModel)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = userRegModel.FirstName;
                newUser.LastName = userRegModel.LastName;
                newUser.Email = userRegModel.Email;
                newUser.Password = userRegModel.Password;
                fundooContext.UserTables.Add(newUser);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return newUser;
                }
                else
                    return null;
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
                var existingLogin = this.fundooContext.UserTables.Where(X => X.Email == userLog.Email && X.Password == userLog.Password).FirstOrDefault();
                if (existingLogin != null)
                {
                    LoginResponseModel login = new LoginResponseModel();
                    string token = GenerateSecurityToken(existingLogin.Email, existingLogin.Id);
                    login.Id = existingLogin.Id;
                    login.FirstName = existingLogin.FirstName;
                    login.LastName = existingLogin.LastName;
                    login.Email = existingLogin.Email;
                    login.Password = existingLogin.Password;
                    login.CreatedAt = existingLogin.CreatedAt;
                    login.Token = token;

                    return login;
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

        
        private string GenerateSecurityToken(string Email, long UserId)  //Generating token
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Appsettings["Jwt:SecKey"])); // Adding a securiy key in appsettings.json
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // identity model for security.
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email), // Access Claim values in controller.
                new Claim("UserId",UserId.ToString())
            };
            var token = new JwtSecurityToken(_Appsettings["Jwt:Issuer"], //we specify the values for the issuer, security key.
             _Appsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),//token time till it will active
              signingCredentials: credentials);
            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ForgetPassword(string email)
        {
            try
            {
                var existingLogin = this.fundooContext.UserTables.Where(X => X.Email == email).FirstOrDefault(); //selecting Email from a table in DB.
                if (existingLogin != null)
                {
                    var token = GenerateSecurityToken(email, existingLogin.Id);
                    new MSMQmodel().MSMQSender(token);
                    return token;
                }
                else
                    return null;
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
                if (password.Equals(confirmPassword)) // comparing of passwords.
                {
                    User user = fundooContext.UserTables.Where(e => e.Email == email).FirstOrDefault();
                    user.Password = confirmPassword;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}    


