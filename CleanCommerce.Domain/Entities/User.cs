using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCommerce.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = [];
        public ICollection<CartItem> CartItems{ get; set; } = [];
        public ICollection<Order> Orders{ get; set; } = [];

    }
}
