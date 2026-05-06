using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Domain.Entities;
using CleanCommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanCommerce.Infrastructure.Repositories
{
    public class RoleRepository(AppDbContext context) : IRoleRepository
    {
        public async Task<Role?> GetByNameAsync(string name)
        {
            return await context.Roles
                .FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<List<string>> GetRoleNamesByUserIdAsync(int userId)
        {
            return await context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(Ur => Ur.Role.Name)
                .ToListAsync();
        }
    }
}
