
using System.ComponentModel.DataAnnotations;

namespace CleanCommerce.Application.DTOs.Requests
{
    public class RegisterUserRequest
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;
    }
}
