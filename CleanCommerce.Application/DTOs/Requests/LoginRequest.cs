using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanCommerce.Application.DTOs.Requests
{
    public class LoginRequest
    {
        [Required]
        public string UsernameOrEmail { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
