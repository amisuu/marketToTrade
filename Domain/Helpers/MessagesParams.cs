namespace Domain.Helpers
{
    public class MessagesParams : PaginationParams
    {
        public string UserName { get; set; }
        public string Container { get; set; } = "Unread";
    }
}
