namespace Domain.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public string SenderUserName { get; set; }
        public AppUser Sender { get; set; }
        public int ReceipientId { get; set; }
        public string ReceipientUserName { get; set; }
        public AppUser Receipient { get; set; }
        public string Content { get; set; }
        public DateTime DateMessageSent { get; set; } = DateTime.UtcNow;
        public DateTime? DateMessageRead { get; set; }
        public bool SenderDeleted { get; set; }
        public bool ReceipientDeleted { get; set; }
    }
}
