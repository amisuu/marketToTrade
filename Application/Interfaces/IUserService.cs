using Application.DTOs;
using Domain.Entities;
using Domain.Helpers;

namespace Application
{
    public interface IUserService
    {
        Task<IEnumerable<MemberDto>> GetAllUsers();
        Task<MemberDto> GetUserById(int id);
        Task<AppUser> GetUserByUsername(string username);
        Task<AppUser> AddUser(AppUser user);
        Task<bool> IsUserExists(string username);
        Task<bool> SaveAllAsync();
        Task<bool> UpdateCurrentUser(UpdateMemberDto memberDto, string currentUser);
        AppUser MapDtoToEntity(RegisterDto registerDto);
        Task<PagedList<MemberDto>> GetMembers(UserParams userParams);
    }
}
