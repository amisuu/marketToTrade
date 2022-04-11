using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application
{
    public interface IUserService
    {
        public Task<IEnumerable<AppUser>> GetAllUsers();
        public Task<AppUser> GetUserById(int id);
        public Task<AppUser> GetUserByUsername(string username);
        public Task<ActionResult<AppUser>> AddUser(AppUser user);
        public Task<bool> IsUserExists(string username);
    }
}
