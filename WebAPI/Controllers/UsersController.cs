using Application;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<AppUser> GetUserById(int id)
        {
            return await _userService.GetUserById(id);
        }
    }
}
