using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LikeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public int AppUserId { get; set; }
        public string Form { get; set; }
        public string Mass { get; set; }
        public string PhotoUrl { get; set; }
    }
}
