using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<MessagesController> _logger;

        public MessagesService(IMessageRepository messageRepository,
                               IMapper mapper,
                               IUserRepository userRepository,
                               ILogger<MessagesController> logger)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
        }

        public ILogger<MessagesController> Logger { get; }

        public async Task<MessageDto> AddMessage(string userName, CreateMessageDto createMessageDto)
        {
            var sender = await _userRepository.GetUserByUsername(userName);
            var recipient = await _userRepository.GetUserByUsername(createMessageDto.RecipientUserName);

            if (recipient == null)
                return null;

            var message = new Message
            {
                Sender = sender,
                Receipient = recipient,
                SenderUserName = sender.Username,
                ReceipientUserName = recipient.Username,
                Content = createMessageDto.Content
            };

            await _messageRepository.AddMessage(message);

            if (await _messageRepository.SaveAllAsync())
                return _mapper.Map<MessageDto>(message);

            return null;
        }

        public async void DeleteMessage(int id, string username)//MessageDto messageDto)
        {
            _messageRepository.DeleteMessage(id);
        }

        public async Task<MessageDto> GetMessage(int id)
        {
            var message = await _messageRepository.GetMessage(id);

            return _mapper.Map<MessageDto>(message);
        }

        public async Task<PagedList<MessageDto>> GetMessagesByUser(MessagesParams messagesParams)
        {
            var query = _messageRepository.GetMessagesForUser(messagesParams);

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, messagesParams.PageNumber, messagesParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName)
        {
            var messages = await _messageRepository.GetMessageThread(currentUserName, recipientUserName);

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _messageRepository.SaveAllAsync();
        }
    }

    public class MessagesController
    {
    }
}
