using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;

namespace Application.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public MessagesService(IMessageRepository messageRepository,
                               IMapper mapper,
                               IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }


        public void AddGroup(Group group)
        {
            _messageRepository.AddGroup(group);
        }

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
                SenderUserName = sender.UserName,
                ReceipientUserName = recipient.UserName,
                Content = createMessageDto.Content
            };

            await _messageRepository.AddMessage(message);

            if (await _messageRepository.SaveAllAsync())
                return _mapper.Map<MessageDto>(message);

            return null;
        }

        public void DeleteMessage(int id, string username)//MessageDto messageDto)
        {
            _messageRepository.DeleteMessage(id);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _messageRepository.GetConnection(connectionId);
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            return await _messageRepository.GetGroupForConnection(connectionId);
        }

        public async Task<MessageDto> GetMessage(int id)
        {
            var message = await _messageRepository.GetMessage(id);

            return _mapper.Map<MessageDto>(message);
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _messageRepository.GetMessageGroup(groupName);
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

        public void RemoveConnection(Connection connection)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _messageRepository.SaveAllAsync();
        }
    }
}
