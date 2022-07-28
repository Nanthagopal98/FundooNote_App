
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
