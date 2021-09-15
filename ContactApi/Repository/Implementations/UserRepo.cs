using DTO;
using Microsoft.AspNetCore.Identity;
using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Repository.Implementations
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<User> _userManager;

        public UserRepo(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        string errors = "";
        public async Task<bool> UpdateUSer(string userId,UpdateUserInfo updateUserInfo)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if(userId !=null)
            {
                user.FirstName    = string.IsNullOrWhiteSpace(updateUserInfo.FirstName) ? user.FirstName : updateUserInfo.FirstName;
                user.LastName     = string.IsNullOrWhiteSpace(updateUserInfo.LastName) ? user.LastName : updateUserInfo.LastName;
                user.PasswordHash = string.IsNullOrWhiteSpace(updateUserInfo.PassWord) ? user.PasswordHash : updateUserInfo.PassWord;
                user.PhoneNumber  = string.IsNullOrWhiteSpace(updateUserInfo.PhoneNumber) ? user.PhoneNumber : updateUserInfo.PhoneNumber;
                user.ImageUrl     = string.IsNullOrWhiteSpace(updateUserInfo.ImageUrl) ? user.ImageUrl : updateUserInfo.ImageUrl;

                var update = await _userManager.UpdateAsync(user);

                if(update.Succeeded)
                {
                    return true;
                }

                foreach (var error in update.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }
                throw new MissingFieldException(errors);
            }
            throw new ArgumentException("User not found");
        }

        public async Task<bool> UserUpdateSelfRecord(UpdateUserInfo updateUserInfo)
        {
            string userId = LoginUser.LoginUserId;
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.FirstName = string.IsNullOrWhiteSpace(updateUserInfo.FirstName) ? user.FirstName : updateUserInfo.FirstName;
                user.LastName = string.IsNullOrWhiteSpace(updateUserInfo.LastName) ? user.LastName : updateUserInfo.LastName;
                user.PasswordHash = string.IsNullOrWhiteSpace(updateUserInfo.PassWord) ? user.PasswordHash : updateUserInfo.PassWord;
                user.PhoneNumber = string.IsNullOrWhiteSpace(updateUserInfo.PhoneNumber) ? user.PhoneNumber : updateUserInfo.PhoneNumber;
                user.ImageUrl = string.IsNullOrWhiteSpace(updateUserInfo.ImageUrl) ? user.ImageUrl : updateUserInfo.ImageUrl;

                var update = await _userManager.UpdateAsync(user);

                if (update.Succeeded)
                {
                    return true;
                }

                foreach (var error in update.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }
                throw new MissingFieldException(errors);
            }
            throw new ArgumentException("User not found");
        }
        public async Task<bool> DeleteUser(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if(user != null)
            {
                IdentityResult deletedUser = await _userManager.DeleteAsync(user);

                if(deletedUser.Succeeded)
                {
                    return true;
                }

                foreach (var error in deletedUser.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }
                throw new MissingFieldException(errors);
            }
            throw new ArgumentException("User not found");
        }

        public async Task<UserSignUpDTO> GetUserById(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                UserSignUpDTO userResult = new UserSignUpDTO();
                userResult.FirstName = user.FirstName;
                userResult.LastName = user.LastName;
                userResult.PhoneNumber = user.PhoneNumber;
                userResult.Email = user.Email;
                return userResult;
            }
            throw new ArgumentException("User not found");
        }

        public async Task<UserSignUpDTO> GetUserByEmail(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                UserSignUpDTO userResult = new UserSignUpDTO();
                userResult.FirstName = user.FirstName;
                userResult.LastName = user.LastName;
                userResult.PhoneNumber = user.PhoneNumber;
                userResult.Email = user.Email;
                return userResult;
            }
            throw new ArgumentException("User not found");
        }

        public IEnumerable<SearchByTermDTO> GetUserBySearchTerm(string SearchTerm)
        {
            var user = _userManager.Users.

                Where(
                 x => x.Email.StartsWith(SearchTerm) ||
                 x.FirstName.StartsWith(SearchTerm) ||
                 x.LastName.StartsWith(SearchTerm) ||
                 x.Id.StartsWith(SearchTerm) ||
                 x.PhoneNumber.StartsWith(SearchTerm));

            List<SearchByTermDTO> results = new List<SearchByTermDTO>();

            if (user != null)
            {
                foreach (var item in user)
                {
                    var result = new SearchByTermDTO
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Id = item.Id,
                        Email = item.Email,
                        PhoneNumber = item.PhoneNumber
                    };

                    results.Add(result);
                }
            }
            return results;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var allUsers = _userManager.Users;
            if(allUsers != null)
            {
                return allUsers;
            }
            throw new ArgumentNullException("Data store is null");

        }
        public async Task<IEnumerable<User>> GetUserAllInRole(string userRole)
        {
            var users = await _userManager.GetUsersInRoleAsync(userRole);
            if (users != null)
            {
                return (users);
            }
            throw new ArgumentNullException("Data store is null");
        }
    }
}