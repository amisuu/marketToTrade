using Application;
using Application.DTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMemberRepository _memberRepository;
        private readonly IPhotoService _photoService;

        public UsersController(IUserService userService,
                               IMemberRepository memberRepository,
                               IPhotoService photoService)
        {
            _userService = userService;
            _memberRepository = memberRepository;
            _photoService = photoService;
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

            if (await _userService.UpdateCurrentUser(memberDto, currentUser))
                return NoContent();

            return BadRequest("Failed to update user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var currentUser = User.GetUsername();
            var user = await _userService.GetUserByUsername(currentUser);

            if (user == null)
                return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var photoDto = await _photoService.GetNewPhotoResult(result, user);

            if (await _userService.SaveAllAsync())
            {
                return CreatedAtAction(nameof(GetUserByUsername), new {username = user.Username}, photoDto);
            }

            return BadRequest("Problem adding photo");
        }
    }
}
