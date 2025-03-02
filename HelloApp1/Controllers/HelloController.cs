using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Service;
using ModelLayer.DTO;
using System;
using System.Threading.Tasks;
using NLog;

namespace HelloApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloAppController : ControllerBase
    {
        private readonly RegisterHelloBL _registerHelloBL;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public HelloAppController(RegisterHelloBL registerHelloBL)
        {
            _registerHelloBL = registerHelloBL;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User newUser)
        {
            try
            {
                var result = await _registerHelloBL.Registration(newUser);
                return Ok(new ResponseModel<string> { Success = true, Message = result });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in RegisterUser");
                return StatusCode(500, new ResponseModel<string> { Success = false, Message = "Internal Server Error" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] User loginUser)
        {
            try
            {
                bool isAuthenticated = await _registerHelloBL.LoginUser(loginUser.Username, loginUser.PasswordHash);

                if (isAuthenticated)
                {
                    return Ok(new ResponseModel<string> { Success = true, Message = "Login Successful", Data = loginUser.Username });
                }

                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid Credentials" });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in LoginUser");
                return StatusCode(500, new ResponseModel<string> { Success = false, Message = "Internal Server Error" });
            }
        }
    }
}
