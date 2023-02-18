namespace Domain.Helpers
{
    public class UserParams : PaginationParams
    {

        private string _search = "";
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
        public string CurrentUsername { get; set; }
        public string KnownAs { get; set; }
        public string OrderBy { get; set; } = "lastActive";
    }
}
