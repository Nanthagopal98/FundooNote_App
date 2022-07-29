
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;
        }
        [HttpPost]
        [Route("Register")]

        public IActionResult RegisterUser(UserRegistrationModel registrationModel)
        {
            try
            {
                var result = iuserBL.UserRegistration(registrationModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Failed" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult UserLogin(UserLoginModel loginModel)
        {
            try
            {
                var result = iuserBL.UserLogin(loginModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failed" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = iuserBL.ForgetPassword(email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Token Sent To Your Email Successfully"});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Token Generation Failed" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = iuserBL.ResetPassword(email, password, confirmPassword);
                if (result != false)
                {
                    return Ok(new { success = true, message = "Password Reset Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password Reset Failed" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
