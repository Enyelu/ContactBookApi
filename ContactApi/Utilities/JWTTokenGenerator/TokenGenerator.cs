
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; 
using System.Text;
using System.Threading.Tasks;

namespace Utilities.JWTTokenGenerator
{
    public class TokenGenerator:ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public TokenGenerator(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        /// <summary>
        /// Token generation to authenticate user
        /// </summary>
        public async Task<string> GenerateToken(User user)
        {
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.FirstName,user.LastName),
                new Claim(ClaimTypes.Email,user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecreteKey"]));

            var token = new JwtSecurityToken
                (
                    audience: _configuration["JWTSettings:Audience"],
                    issuer: _configuration["JWTSettings:Issuer"],
                    claims: userClaims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials : new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
