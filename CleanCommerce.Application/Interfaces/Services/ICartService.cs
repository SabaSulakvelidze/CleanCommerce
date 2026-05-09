
using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;

namespace CleanCommerce.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartReposnse> GetMyCartAsync();
        Task<CartReposnse> AddToCartAsync(AddToCartRequest request);
        Task<CartReposnse> UpdateCartItemASync(int cartItemId, UpdateCartItemRequest request);
        Task<bool> RemoveCartItemAsync(int cartItemId);
        Task<bool> ClearMyCartAsync();
    }
}
