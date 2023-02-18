using Application;
using Application.DTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Helpers;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
        {
            var user = await _userService.GetUserByUsername(User.GetUsername());
            userParams.CurrentUsername = user.Username;
            userParams.KnownAs = user.KnownAs;

            var users = await _userService.GetMembers(userParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

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
            var currentUser = User.GetUsername();

            var user = await _userService.GetUserByUsername(currentUser);

            if (await _userService.UpdateUser(memberDto, user))
            {
                return NoContent();
            }

            return BadRequest("Failed to update user");
        }
    }
}
