using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MessagesService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public void AddGroup(Group group)
        {
            _unitOfWork.MessageRepository.AddGroup(group);
        }

        public async Task<MessageDto> AddMessage(string userName, CreateMessageDto createMessageDto)
        {
            var sender = await _unitOfWork.UserRepository.GetUserByUsername(userName);
            var recipient = await _unitOfWork.UserRepository.GetUserByUsername(createMessageDto.RecipientUserName);

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

            await _unitOfWork.MessageRepository.AddMessage(message);

            if (await _unitOfWork.Complete())
                return _mapper.Map<MessageDto>(message);

            return null;
        }

        public void DeleteMessage(int id, string username)//MessageDto messageDto)
        {
            _unitOfWork.MessageRepository.DeleteMessage(id);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _unitOfWork.MessageRepository.GetConnection(connectionId);
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            return await _unitOfWork.MessageRepository.GetGroupForConnection(connectionId);
        }

        public async Task<MessageDto> GetMessage(int id)
        {
            var message = await _unitOfWork.MessageRepository.GetMessage(id);

            return _mapper.Map<MessageDto>(message);
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _unitOfWork.MessageRepository.GetMessageGroup(groupName);
        }

        public async Task<PagedList<MessageDto>> GetMessagesByUser(MessagesParams messagesParams)
        {
            var messages = _unitOfWork.MessageRepository.GetMessagesForUser(messagesParams)
                                                        .OrderByDescending(m => m.DateMessageSent)
                                                        .ProjectTo<MessageDto>(_mapper.ConfigurationProvider);                                                   

            return await PagedList<MessageDto>.CreateAsync(messages, messagesParams.PageNumber, messagesParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName)
        {
            var messages = await _unitOfWork.MessageRepository.GetMessageThread(currentUserName, recipientUserName)
                                                           .Where(m => m.ReceipientUserName == currentUserName &&
                                                                  m.ReceipientDeleted == false &&
                                                                  m.SenderUserName == recipientUserName ||
                                                                  m.ReceipientUserName == recipientUserName &&
                                                                  m.SenderDeleted == false &&
                                                                  m.SenderUserName == currentUserName)
                                                           .OrderBy(m => m.DateMessageSent)
                                                           .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync();

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public void RemoveConnection(Connection connection)
        {
            _unitOfWork.MessageRepository.RemoveConnection(connection);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _unitOfWork.Complete();
        }
    }
}
