using ICBF.Domain.DTO;
using ICBF.Domain.Models;

namespace ICBF.Application.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(AppUser user);
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserDtoByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<bool> UserExists(string username);
    }
}
