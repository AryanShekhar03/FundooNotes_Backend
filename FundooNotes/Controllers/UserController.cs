using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")] //Route is  matching incoming HTTP requests.
    [ApiController]//To enable Routing Requirements.
    public class UserController : ControllerBase //To handle http request
    {
        private readonly IUserBL userBL; // readonly can only be assigned a value from within the constructor(s) of a class.
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult addUser(UserRegModel userRegModel) //IActionResult lets you return both data and HTTP codes.
        {
            try
            {
                var result = userBL.Registration(userRegModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                    return this.BadRequest(new { success = false, message = "Registration Unsuccessful" });
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost("AllLogin")]
        public IActionResult UserLogin(UserLoginModel userLog)
        {
            try
            {
                var result = userBL.UserLogin(userLog);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                    return this.BadRequest(new { success = false, message = "Login Unsuccessful" });
            }
            catch (Exception)
            {
                throw;
            }
    
        }
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)  //IActionResult let us return both data and HTTP codes.
        {
            try
            {
                var result = userBL.ForgetPassword(email);
                if (result != null)
                {
                    return this.Ok(new { isSuccess = true, message = "Send Forget Password Link" });
                }
                else
                    return this.BadRequest(new { isSuccess = false, message = "Email not Found" });
            }
            catch (Exception e)
            {

                return this.BadRequest(new { isSuccess = false, message = e.InnerException.Message });
            }
        }

        [Authorize]
        [HttpPost("ResetPassword")]

        public IActionResult ResetPassword(string password, string confirmPassword)  //IActionResult let us return both data and HTTP codes.
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetPassword(email, password, confirmPassword);
                return this.Ok(new { isSuccess = true, message = "Reset Password Successfully" });

            }
            catch (Exception e)
            {

                return this.BadRequest(new { isSuccess = false, message = e.InnerException.Message });
            }
        }


    }
}

