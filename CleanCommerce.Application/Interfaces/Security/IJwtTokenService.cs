using CleanCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCommerce.Application.Interfaces.Security
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user, List<string> roles);
    }
}
