using Domain.Entities;
using Domain.Helpers;

namespace Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task AddMessage(Message message);
        Task DeleteMessage(int id);
        Task<Message> GetMessage(int id);
        IQueryable<Message> GetMessagesForUser(MessagesParams messagesParams);
        Task<IEnumerable<Message>> GetMessageThread(string currentUserName, string recipientUserName);
        Task<bool> SaveAllAsync();
    }
}
