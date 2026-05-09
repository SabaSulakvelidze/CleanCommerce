
using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Domain.Entities;
using CleanCommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanCommerce.Infrastructure.Repositories
{
    public class CartRepository(AppDbContext context) : ICartRepository
    {
        public async Task<CartItem> AddAsync(CartItem cartItem)
        {
            context.CartItems.Add(cartItem);
            await context.SaveChangesAsync();

            await context.Entry(cartItem)
                .Reference(ci => ci.Product)
                .LoadAsync();

            return cartItem;
        }

        public async Task ClearAsync(List<CartItem> cartItem)
        {
            context.CartItems.RemoveRange(cartItem);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CartItem cartItem)
        {
            context.CartItems.Remove(cartItem);
            await context.SaveChangesAsync();
        }

        public async Task<CartItem?> GetByIdAsync(int cartId)
        {
            return await context.CartItems
                .Include(ci=>ci.Product)
                .FirstOrDefaultAsync(ci => ci.Id == cartId);
        }

        public async Task<CartItem?> GetByUserIdAndProductIdAsync(int userId, int productId)
        {
            return await context.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);
        }

        public Task<List<CartItem>> GetByUserIdAsync(int userId)
        {
            return context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();
        }

        public async Task<CartItem> Updatesync(CartItem cartItem)
        {
            context.CartItems.Update(cartItem);
            await context.SaveChangesAsync();

            await context.Entry(cartItem)
                .Reference(c => c.Product)
                .LoadAsync();
            return cartItem;
        }
    }
}
