using Domain.Entities;
using Domain.Helpers;

namespace Domain.Interfaces
{
    public interface IMessageRepository
    {
        void AddGroup(Group group);
        Task AddMessage(Message message);
        Task DeleteMessage(int id);
        Task<Connection> GetConnection(string connectionId);
        Task<Group> GetGroupForConnection(string connectionId);
        Task<Message> GetMessage(int id);
        IQueryable<Message> GetMessagesForUser(MessagesParams messagesParams);
        Task<Group> GetMessageGroup(string groupName);
        IQueryable<Message> GetMessageThread(string currentUserName, string recipientUserName);
        void RemoveConnection(Connection connection);
    }
}
