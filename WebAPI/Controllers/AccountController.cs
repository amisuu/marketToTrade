using Application;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService, ITokenService tokenService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await IsExists(registerDto.Username))
            {
                return BadRequest("Username is taken, choose other");
            }

            var user = _userService.RegisterUser(registerDto);

            user.UserName = registerDto.Username.ToLower();

            var createResult = await _userManager.CreateAsync(user, registerDto.Password);

            if (!createResult.Succeeded) 
            {
                return BadRequest(createResult.Errors);
            }

            var roleReslut = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleReslut.Succeeded)
            {
                return BadRequest(roleReslut.Errors);
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.Include(x => x.Photos)
                                               .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            var passwordResult = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!passwordResult)
            {
                return Unauthorized("Inwalid username or password.");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
            };
        }

        private async Task<bool> IsExists(string username)
        {
            return await _userService.IsUserExists(username);
        }
    }
}
