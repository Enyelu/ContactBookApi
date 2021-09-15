using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.JWTTokenGenerator
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}
