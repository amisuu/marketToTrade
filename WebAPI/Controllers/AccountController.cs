using Application;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await IsExists(registerDto.Username))
            {
                return BadRequest("Username is taken, choose other");
            }

            return await _userService.RegisterUser(registerDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.GetUserByUsername(loginDto.Username);

            if (user == null)
                return Unauthorized("Invalid username");

            var result = await _userService.LoginUser(loginDto);

            if (result == null)
                return Unauthorized("Invalid password or username");

            return result;
        }

        private async Task<bool> IsExists(string username)
        {
            return await _userService.IsUserExists(username);
        }
    }
}
