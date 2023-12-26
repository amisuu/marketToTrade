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
            userParams.CurrentUsername = user.UserName;
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

            var userDto = new AppUserDto { Username = user.UserName };

            if (user == null)
                return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var photoDto = await _photoService.GetNewPhotoResult(result, userDto, null);

            if (await _userService.SaveAllAsync())
            {
                //return photoDto;
                return CreatedAtAction(nameof(GetUserByUsername), new { username = userDto.Username }, photoDto);
            }

            return BadRequest("Problem with adding photo.");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var currentUser = User.GetUsername();
            var user = await _userService.GetUserByUsername2(currentUser);

            if (user == null)
                return NoContent();

            var photo = user.Photos.FirstOrDefault(u => u.Id == photoId);

            if (photo == null)
                return NotFound();

            if (photo.IsMain)
                return BadRequest("Deleting main photo is prohibited.");

            if (!string.IsNullOrEmpty(photo.PublicId))
            {
                var result = await _photoService.DeletePhotoAsync2(photo.PublicId);


                user.Photos.Remove(photo);

                if (result.Error != null)
                    return BadRequest(result.Error.Message);
            }
            //await _userService.SaveAllAsync();
            //return Ok();
            if (await _userService.SaveAllAsync())
                return Ok();

            return BadRequest();
        }
    }
}
