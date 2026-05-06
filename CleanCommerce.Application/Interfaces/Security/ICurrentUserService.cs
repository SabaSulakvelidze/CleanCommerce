using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCommerce.Application.Interfaces.Security
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        string? Username{ get; }
        List<string> Roles{ get; }
        bool IsAuthenticated { get; }
    }
}
