
using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Domain.Entities;
using CleanCommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanCommerce.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User> AddAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.Email == email.Trim().ToLowerInvariant());
        }

        public async Task<User?> GetByIdAsunc(int id)
        {
            return await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.Username == username.Trim());
        }

        public async Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == usernameOrEmail.Trim().ToLowerInvariant() ||
                u.Email == usernameOrEmail.Trim().ToLowerInvariant());
        }
    }
}
