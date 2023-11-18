using Application.DTOs;
using Domain.Helpers;

namespace Application.Interfaces
{
    public interface IMessagesService
    {
        Task<MessageDto> AddMessage(string userName, CreateMessageDto createMessageDto);
        void DeleteMessage(int id, string username);
        Task<MessageDto> GetMessage(int id);
        Task<PagedList<MessageDto>> GetMessagesByUser(MessagesParams messagesParams);
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName);
        Task<bool> SaveAllAsync();
    }
}
