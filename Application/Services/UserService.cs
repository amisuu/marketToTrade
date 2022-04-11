using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _userRepository.GetUsers();
        }
        public async Task<AppUser> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }

        public async Task<ActionResult<AppUser>> AddUser(AppUser user)
        {
            await _userRepository.Add(user);

            return user;
        }

        public async Task<bool> IsUserExists(string username)
        {
            return await _userRepository.IsExists(username);
        }
    }
}
