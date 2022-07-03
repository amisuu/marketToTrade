using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class RegisterDto
    {
        [Required] public string Username { get; set; }
        [Required] public string KnownAs { get; set; }
        [Required] public int Phone { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}
