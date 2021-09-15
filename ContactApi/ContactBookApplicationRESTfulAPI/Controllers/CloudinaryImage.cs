using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository.Interfaces;
using System;
using System.Threading.Tasks;
using Utilities;

namespace ContactBookApplicationRESTfulAPI.Controllers
{
    
    [ApiController]
    public class CloudinaryImage : ControllerBase
    {
        private readonly ICloudinaryImageRepo _cloudinaryImageRepo;
        private readonly UserManager<User> _userManager;
        public CloudinaryImage(ICloudinaryImageRepo cloudinaryImageRepo, UserManager<User> userManager)
        {
            _cloudinaryImageRepo = cloudinaryImageRepo;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("api/[controller]/UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] CloudinaryImageeDTO cloudinaryImageeDTO)
        {
            try
            {
                var uploadUploaded = await _cloudinaryImageRepo.UploadAsync(cloudinaryImageeDTO.Image);
                var imageUrl = uploadUploaded.Url.ToString();

                var userId = LoginUser.LoginUserId;
                var user = await _userManager.FindByIdAsync(userId);
                user.ImageUrl = imageUrl;
                await _userManager.UpdateAsync(user);


                return Ok(imageUrl);
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
   }
}
