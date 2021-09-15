using DTO;
using DTO.Mappings;
using Microsoft.AspNetCore.Identity;
using Models;
using Repository.Interfaces;
using System;
using System.Threading.Tasks;
using Utilities;
using Utilities.JWTTokenGenerator;

namespace Repository.Implementations
{
    public class AuthenticationRepo: IAuthenticationRepo
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        public AuthenticationRepo(UserManager<User> userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginDTO2 userDTO2)
        {
            User user = await _userManager.FindByEmailAsync(userDTO2.Email);
            if(user != null)
            {
                LoginUser.LoginUserId = user.Id;
                if (await _userManager.CheckPasswordAsync(user,userDTO2.PassWord))
                {
                    var result = UserMappings.UserResponse(user);
                    result.Token = await _tokenGenerator.GenerateToken(user);
                    return result;
                }
                throw new AccessViolationException("Username or Password is invalid");
            }
            throw new AccessViolationException("Username or Password is invalid");
        }

        public async Task<UserLoginResponseDTO>  SignUp(UserSignUpDTO userSignUpDTO)
        {
            User user = UserMappings.RegisterUser(userSignUpDTO);
            IdentityResult create = await _userManager.CreateAsync(user, userSignUpDTO.PassWord);
                                    await _userManager.AddToRoleAsync(user, "Regular");

            if (create.Succeeded)
            {
                return UserMappings.UserResponse(user);
            }
            string errors = "";

            foreach (var error in create.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }
            throw new MissingFieldException(errors);
        }
    }
}
