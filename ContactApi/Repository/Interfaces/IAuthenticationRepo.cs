﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAuthenticationRepo
    {
        Task<UserLoginResponseDTO> Login(UserLoginDTO2 userDTO2);
        Task<UserLoginResponseDTO> SignUp(UserSignUpDTO userSignUpDTO);
    }
}
