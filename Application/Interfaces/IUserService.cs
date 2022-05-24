using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application
{
    public interface IUserService
    {
        public Task<IEnumerable<MemberDto>> GetAllUsers();
        public Task<MemberDto> GetUserById(int id);
        public Task<AppUser> GetUserByUsername(string username);
        public Task<AppUser> AddUser(AppUser user);
        public Task<bool> IsUserExists(string username);
        public Task<bool> UpdateUser(UpdateMemberDto memberDto, AppUser user);
    }
}
