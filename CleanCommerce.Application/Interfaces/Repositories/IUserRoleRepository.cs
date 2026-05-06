using CleanCommerce.Domain.Entities;

namespace CleanCommerce.Application.Interfaces.Repositories
{
    public interface IUserRoleRepository
    {
        Task TaskAsync(UserRole userRole);
    }
}
