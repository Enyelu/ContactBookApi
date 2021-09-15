using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace ContactBookApplicationRESTfulAPI.Controllers
{
    [ApiController]
    //This class handles the crud operations of users. Authentication and Authorization has been implemented to grant access to actual users.
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        
        [HttpGet]
        [Route("api/[controller]/GetUserById")]
        [Authorize (Roles ="Admin")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                return Ok(await _userRepo.GetUserById(id));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpGet]
        [Route("api/[controller]/GetUserByEmail")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                return Ok(await _userRepo.GetUserByEmail(email));
            }
            catch (ArgumentException e)
            {

                return NotFound(e.Message);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("api/[controller]/AdminUpdateUSer")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AdminUpdateUSer(string userId, UpdateUserInfo updateUserInfo)
        {
            try
            {
                var update = await _userRepo.UpdateUSer(userId, updateUserInfo);
                return Ok("Update Successful");
                
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("api/[controller]/UserUpdateRecord")]
        [Authorize(Roles = "Admin,Regular")]
        public async Task<IActionResult> UserUpdateRecord(UpdateUserInfo updateUserInfo)
        {
            try
            {
                var update = await _userRepo.UserUpdateSelfRecord(updateUserInfo);
                return Ok("Update Successful");
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetAllUsers")]
        [Authorize (Roles ="Admin")]
        public IActionResult GetAllUsers([FromQuery] Pagenator userInput)
        {
            var userId = LoginUser.LoginUserId;
            try
            {
                var pagenator = new Pagenator(userInput.PageSize, userInput.CurrentPage);
                return Ok(_userRepo.GetAllUsers().Skip((pagenator.CurrentPage - 1) * pagenator.PageSize).Take(pagenator.PageSize).ToArray());
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetUserBySearchTerm")]
        [Authorize(Roles = "Admin,Regular")]
        public IActionResult GetUserBySearchTerm(string SearchTerm, [FromQuery] Pagenator userInput)
        {
            var pagenator = new Pagenator(userInput.PageSize, userInput.CurrentPage);
            return Ok(_userRepo.GetUserBySearchTerm(SearchTerm).Skip((pagenator.CurrentPage - 1) * pagenator.PageSize).Take(pagenator.PageSize).ToArray());
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteUSer")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                var delete = await _userRepo.DeleteUser(userId);
                return Ok("User Deleted Successfully");
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetAllUSerInRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUserInRole(string userRole)
        {
            try
            {
                return Ok(await _userRepo.GetUserAllInRole(userRole));
            }
            catch (NullReferenceException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
