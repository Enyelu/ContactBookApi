using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace ContactBookApplicationRESTfulAPI.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepo _authentication;
       
        public AuthenticationController(IAuthenticationRepo authentication)
        {
            _authentication = authentication;
        }


        [HttpPost]
        [Route("api/[controller]/Login")]
        public async Task<IActionResult> Login(UserLoginDTO2 userLoginDTO2)
        {
            try
            {
                return Ok(await _authentication.Login(userLoginDTO2));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500) ;
            }
        }

        [HttpPost]
        [Route("api/[controller]/SignUp")]
        public async Task<IActionResult> SignUP(UserSignUpDTO userSignUpDTO)
        {
            try
            {
                return Ok(await _authentication.SignUp(userSignUpDTO));
            }
            catch (MissingFieldException)
            {
                return BadRequest("Some fields are missing");
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }

}
