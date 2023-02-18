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

            var user = _userService.MapDtoToEntity(registerDto);

            using var hmac = new HMACSHA256();

            user.Username = registerDto.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;

            await _userService.AddUser(user);
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.GetUserByUsername(loginDto.Username);

            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            using var hmac = new HMACSHA256(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid password or username");
                }
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
            };
        }

        private async Task<bool> IsExists(string username)
        {
            return await _userService.IsUserExists(username);
        }
    }
}
