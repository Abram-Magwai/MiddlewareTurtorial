using System.ComponentModel.DataAnnotations;

namespace MiddlewareTurtorial.Models
{
    public class Login
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
