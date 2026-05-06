using BCrypt.Net;
using CleanCommerce.Application.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCommerce.Infrastructure.Security
{
    public class PaswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
