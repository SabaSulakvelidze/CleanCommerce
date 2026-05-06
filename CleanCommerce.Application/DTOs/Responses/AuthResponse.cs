using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCommerce.Application.DTOs.Responses
{
    public class AuthResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<string> Roles { get; set; } = [];
        public string Token { get; set; } = null!;
    }
}
