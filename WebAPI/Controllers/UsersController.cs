using Application;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMemberRepository _memberRepository;

        public UsersController(IUserService userService, 
                               IMemberRepository memberRepository)
        {
            _userService = userService;
            _memberRepository = memberRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _memberRepository.GetMembers();

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<MemberDto> GetUserById(int id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByUsername(string username)
        {
            return await _memberRepository.GetMemberByUsername(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateMemberDto memberDto)
        {
            var currentUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userService.GetUserByUsername(currentUser);

            if (await _userService.UpdateUser(memberDto, user))
            {
                return NoContent();
            }

            return BadRequest("Failed to update user");
        }

        private string GetUsername()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
