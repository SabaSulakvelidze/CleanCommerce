using CleanCommerce.Application.Interfaces.Security;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanCommerce.Infrastructure.Security
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public int? UserId
        {
            get
            {
                var value = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(value, out var userId))
                    return userId;
                return null;
            }
        }

        public string? Username => httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

        public List<string> Roles => httpContextAccessor.HttpContext?.User?
            .FindAll(ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList() ?? [];

        public bool IsAuthenticated => httpContextAccessor.HttpContext?.User?.Identity?
            .IsAuthenticated ?? false;
    }
}
