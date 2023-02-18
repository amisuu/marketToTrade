using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<AppUser>> GetUsers();
        public Task<AppUser> GetUserById(int id);
        public Task<AppUser> GetUserByUsername(string username);
        public Task<AppUser> Add(AppUser user);
        public Task<bool> IsExists(string username);
        public Task<bool> SaveAllAsync();
        public void Update(AppUser user);
    }
}
