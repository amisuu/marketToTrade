using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsers();
        Task<AppUser> GetUserById(int id);
        Task<AppUser> GetUserByUsername(string username);
        Task<AppUser> Add(AppUser user);
        Task<bool> IsExists(string username);
        void Update(AppUser user);
    }
}
