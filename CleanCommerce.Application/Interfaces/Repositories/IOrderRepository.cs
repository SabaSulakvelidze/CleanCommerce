
using CleanCommerce.Domain.Entities;

namespace CleanCommerce.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddSync(Order order);
        Task<Order?> GetByIdAsyunc(int orderId);
        Task<List<Order>> GetByUserIdAsync(int userId);
        Task<List<Order>> GetAllAsync();
        Task<Order> UpdateAsync(Order order);
    }
}
