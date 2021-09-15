using CloudinaryDotNet.Actions;
using DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICloudinaryImageRepo
    {
        Task<UploadResult> UploadAsync(IFormFile cloudinaryImage);
    }
}
