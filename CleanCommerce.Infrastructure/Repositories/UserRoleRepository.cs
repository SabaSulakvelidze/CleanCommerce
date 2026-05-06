
using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Domain.Entities;
using CleanCommerce.Infrastructure.Persistence;

namespace CleanCommerce.Infrastructure.Repositories
{
    public class UserRoleRepository(AppDbContext context) : IUserRoleRepository
    {
        public async Task TaskAsync(UserRole userRole)
        {
            context.UserRoles.Add(userRole);
            await context.SaveChangesAsync();
        }
    }
}
