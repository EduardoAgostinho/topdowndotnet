using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
        Task<bool> UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUserAsync(string id);
    }
}