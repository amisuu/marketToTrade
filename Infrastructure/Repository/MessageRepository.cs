using Application.Services;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MessagesController> _logger;

        public MessageRepository(ApplicationDbContext context, ILogger<MessagesController> logger)
        {
            _context = context;
            _logger = logger;
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

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public IQueryable<Message> GetMessagesForUser(MessagesParams messagesParams)
        {
            var query = _context.Messages.OrderByDescending(m => m.DateMessageSent)
                                               .AsQueryable();

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

        public async Task<IEnumerable<Message>> GetMessageThread(string currentUserName, string recipientUserName)
        {
            var messages = await _context.Messages.Include(m => m.Sender).ThenInclude(p => p.Photos)
                                                  .Include(m => m.Receipient).ThenInclude(p => p.Photos)
                                                  .Where(m => m.ReceipientUserName == currentUserName &&
                                                         m.ReceipientDeleted == false &&
                                                         m.SenderUserName == recipientUserName || 
                                                         m.ReceipientUserName == recipientUserName &&
                                                         m.SenderDeleted == false &&
                                                         m.SenderUserName == currentUserName)
                                                  .OrderBy(m => m.DateMessageSent)
                                                  .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateMessageRead == null &&
                                                m.ReceipientUserName == currentUserName)
                                         .ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateMessageRead = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
            }

            return messages;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
