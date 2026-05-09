
using AutoMapper.Configuration.Conventions;
using CleanCommerce.Domain.Entities;

namespace CleanCommerce.Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartItem>> GetByUserIdAsync(int userId);
        Task<CartItem?> GetByIdAsync(int cartId);
        Task<CartItem?> GetByUserIdAndProductIdAsync(int userId,int productId);
        Task<CartItem> AddAsync(CartItem cartItem);
        Task<CartItem> Updatesync(CartItem cartItem);
        Task DeleteAsync(CartItem cartItem);
        Task ClearAsync(List<CartItem> cartItem);
    }
}
