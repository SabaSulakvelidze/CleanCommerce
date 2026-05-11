
using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Domain.Entities;
using CleanCommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanCommerce.Infrastructure.Repositories
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        public Task<Order> AddSync(Order order)
        {
            context.Orders.Add(order);

            return Task.FromResult(order);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetByUserIdAsync(int userId)
        {
            return await context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();

            return order;
        }
    }
}
