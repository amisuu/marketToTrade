using Domain.Entities;

namespace Application.DTOs
{
    public class UpdateMemberDto
    {
        public string KnownAs { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
    }
}
