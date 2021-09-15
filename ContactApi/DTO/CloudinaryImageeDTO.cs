using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CloudinaryImageeDTO
    {
        public IFormFile Image { get; set; }
    }
}
