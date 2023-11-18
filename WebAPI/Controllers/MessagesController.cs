using Application;
using Application.DTOs;
using Application.Extensions;
using Application.Helpers;
using Application.Interfaces;
using Domain.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class MessagesController : BaseController
    {
        private readonly IMessagesService _messagesService;
        private readonly IUserService _userService;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(IMessagesService messagesService,
                                  IUserService userService,
                                  ILogger<MessagesController> logger)
        {
            _messagesService = messagesService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var userName = User.GetUsername();

            if (userName == createMessageDto.RecipientUserName.ToLower())
                return BadRequest("You can't send messages to yourself.");

            var result = await _messagesService.AddMessage(userName, createMessageDto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MessageDto>>> GetMessagesForUser([FromQuery] MessagesParams messagesParams)
        {
            messagesParams.UserName = User.GetUsername();

            var messages = await _messagesService.GetMessagesByUser(messagesParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);

            return messages;
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string recipientUserName)
        {
            var currentUser = User.GetUsername();

            return Ok(await _messagesService.GetMessageThread(currentUser, recipientUserName));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = "wujek";// User.GetUsername();

            var message = await _messagesService.GetMessage(id);
            
            if (message.SenderUserName != username && message.ReceipientUserName != username)
                return Unauthorized();

            _messagesService.DeleteMessage(id, username);

            if (await _messagesService.SaveAllAsync())
                return Ok();

            return BadRequest("System cannot delete this message.");
        }
    }
}
