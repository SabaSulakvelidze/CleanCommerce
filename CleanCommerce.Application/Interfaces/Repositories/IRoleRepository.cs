using CleanCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCommerce.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByNameAsync(string name);
        Task<List<string>> GetRoleNamesByUserIdAsync(int userId);
    }
}
