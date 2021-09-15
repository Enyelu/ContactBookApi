using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Mappings
{
    public class UserMappings
    {
        public static UserLoginResponseDTO UserResponse(User user )
        {
            return new UserLoginResponseDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
        }

        public static User RegisterUser(UserSignUpDTO userSignUpDTO)
        {
            return new User
            {
                FirstName = userSignUpDTO.FirstName,
                LastName = userSignUpDTO.LastName,
                Email = userSignUpDTO.Email,
                PhoneNumber = userSignUpDTO.PhoneNumber,
                UserName = string.IsNullOrWhiteSpace(userSignUpDTO.UserName) ? userSignUpDTO.Email : userSignUpDTO.UserName
            };
        }
    }
}
