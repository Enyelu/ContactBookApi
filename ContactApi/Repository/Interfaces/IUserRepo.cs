using DTO;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> UpdateUSer(string userId, UpdateUserInfo updateUserInfo);
        Task<bool> DeleteUser(string userId);
        Task<UserSignUpDTO> GetUserById(string userId);
        IEnumerable <User> GetAllUsers();
        IEnumerable<SearchByTermDTO> GetUserBySearchTerm(string SearchTerm);
        Task<IEnumerable<User>> GetUserAllInRole(string userRole);
        Task<UserSignUpDTO> GetUserByEmail(string email);
        Task<bool> UserUpdateSelfRecord(UpdateUserInfo updateUserInfo);
    }
}
