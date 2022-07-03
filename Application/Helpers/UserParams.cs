namespace Application.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        private string _search = "";

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

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
