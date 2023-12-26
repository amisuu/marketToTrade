using Application.DTOs;
using Domain.Entities;
using Domain.Helpers;

namespace Application.Interfaces
{
    public interface IMessagesService
    {
        void AddGroup(Group group);
        Task<MessageDto> AddMessage(string userName, CreateMessageDto createMessageDto);
        void DeleteMessage(int id, string username);
        Task<Connection> GetConnection(string connectionId);
        Task<Group> GetGroupForConnection(string connectionId);
        Task<MessageDto> GetMessage(int id);
        Task<PagedList<MessageDto>> GetMessagesByUser(MessagesParams messagesParams);
        Task<Group> GetMessageGroup(string groupName);
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName);
        void RemoveConnection(Connection connection);
        Task<bool> SaveAllAsync();
    }
}
