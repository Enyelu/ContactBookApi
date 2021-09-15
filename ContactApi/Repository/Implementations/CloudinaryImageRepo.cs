using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace Repository.Implementations
{
    public class CloudinaryImageRepo:ICloudinaryImageRepo
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private readonly ImageUploadSettings _imageUploadSettings; 
        public CloudinaryImageRepo(IConfiguration configuration, IOptions<ImageUploadSettings> imageUploadSettings)
        {
            _imageUploadSettings = imageUploadSettings.Value;
            _configuration = configuration;
            _cloudinary = new Cloudinary(new Account(_imageUploadSettings.CloudName,_imageUploadSettings.ApiKey,_imageUploadSettings.ApiSecret));
        }
        public async Task<UploadResult> UploadAsync(IFormFile image)
        {
            var imageFormat = false;
            var listOfImageExtensions = _configuration.GetSection("ImageFormatSettings:Format").Get<List<string>>();

            foreach (var item in listOfImageExtensions )
            {
                if (image.FileName.EndsWith(item))
                {
                    imageFormat = true;
                    break;
                }
            }
            if(imageFormat == false)
            {
                throw new ArgumentException("File format not supported");
            }

            var uploadImage = new ImageUploadResult();
           

            using (var imageStream = image.OpenReadStream())
            {
                string fileName = Guid.NewGuid().ToString() + image.FileName;
                uploadImage = await _cloudinary.UploadAsync(new ImageUploadParams()
                { 
                    File = new FileDescription(fileName, imageStream),
                    Transformation = new Transformation()
                        .Crop("thumb")
                        .Gravity("face")
                        .Width(150)
                        .Height(200)
                        .Radius(5)
                });
            }
           
            return uploadImage; 
        }
    }
}
