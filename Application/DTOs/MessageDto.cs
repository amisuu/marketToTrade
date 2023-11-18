namespace Application.DTOs
{
    public class MessageDto
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public string SenderUserName { get; set; }
        public string SenderPhotoUrl { get; set; }
        public int ReceipientId { get; set; }
        public string ReceipientUserName { get; set; }
        public string ReceipientPhotoUrl { get; set; }
        public string Content { get; set; }
        public DateTime DateMessageSent { get; set; }
        public DateTime? DateMessageRead { get; set; }
    }
}
