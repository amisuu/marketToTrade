using Application.DTOs;
using Application.Helpers;
using Domain.Entities;
using Domain.Helpers;

namespace Application
{
    public interface IUserService
    {
        public Task<IEnumerable<MemberDto>> GetAllUsers();
        public Task<MemberDto> GetUserById(int id);
        public Task<AppUser> GetUserByUsername(string username);
        public Task<AppUser> AddUser(AppUser user);
        public Task<bool> IsUserExists(string username);
        public Task<bool> SaveAllAsync();
        public Task<bool> UpdateUser(UpdateMemberDto memberDto, AppUser user);
        public AppUser MapDtoToEntity(RegisterDto registerDto);
        public Task<PagedList<MemberDto>> GetMembers(UserParams userParams);
    }
}
