using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        public async Task AddMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task DeleteMessage(int id)
        {
            var message = await GetMessage(id);

            _context.Messages.Remove(message);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _context.Connections.FindAsync(connectionId);
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            return await _context.Groups.Include(x => x.Connections)
                                        .Where(x => x.Connections.Any(c => c.ConnectionId == connectionId))
                                        .FirstOrDefaultAsync();
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _context.Groups.Include(x => x.Connections).FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public IQueryable<Message> GetMessagesForUser(MessagesParams messagesParams)
        {
            var query = _context.Messages.AsQueryable();

            query = messagesParams.Container switch
            {
                "Inbox" => query.Where(m => m.ReceipientUserName == messagesParams.UserName &&
                                       m.ReceipientDeleted == false),
                "Outbox" => query.Where(m => m.SenderUserName == messagesParams.UserName &&
                                        m.SenderDeleted == false),
                _ => query.Where(m => m.ReceipientUserName == messagesParams.UserName &&
                                 m.DateMessageRead == null &&
                                 m.ReceipientDeleted == false)
            };

            return query;
        }

        public IQueryable<Message> GetMessageThread(string currentUserName, string recipientUserName)
        {
            var messages = _context.Messages.AsQueryable();

            var unreadMessages = messages.Where(m => m.DateMessageRead == null &&
                                                m.ReceipientUserName == currentUserName).ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateMessageRead = DateTime.UtcNow;
                }
            }

            return messages;
        }

        public void RemoveConnection(Connection connection)
        {
            _context.Connections.Remove(connection);
        }
    }
}
