
using AutoMapper;
using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;
using CleanCommerce.Application.Exceptions;
using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Application.Interfaces.Security;
using CleanCommerce.Application.Interfaces.Services;
using CleanCommerce.Domain.Entities;
using System.Globalization;

namespace CleanCommerce.Application.Services
{
    public class CartService(
        ICartRepository cartRepository,
        IProductRepository productRepository,
        ICurrentUserService currentUserService,
        IMapper mapper
        ) : ICartService
    {
        public async Task<CartReposnse> AddToCartAsync(AddToCartRequest request)
        {
            var userid = GetCurrentUserId();

            var product = await productRepository.GetByIdAsync(request.ProductId) ?? 
                throw new NotFoundException("Product not found.");

            if (product.StockQuantity < request.Quantity)
                throw new BadRequestException("Requested quantity exeeds available stock.");

            var existingCartItem = await cartRepository
                .GetByUserIdAndProductIdAsync(userid, request.ProductId);

            if (existingCartItem is not null)
            {
                var newQuantity = existingCartItem.Quantity + request.Quantity;

                if (product.StockQuantity < newQuantity)
                    throw new BadRequestException("Requested quantity exceeds available stock.");

                existingCartItem.Quantity = newQuantity;

                await cartRepository.Updatesync(existingCartItem);
            }
            else
            {
                var cartItem = mapper.Map<CartItem>(request);
                cartItem.UserId = userid;

                await cartRepository.AddAsync(cartItem);
            }

            return BuildCartResponse(userid,await cartRepository.GetByUserIdAsync(userid));
        }

        public async Task<bool> ClearMyCartAsync()
        {
            var userId = GetCurrentUserId();

            var cartItms = await cartRepository.GetByUserIdAsync(userId);

            if (cartItms.Count == 0)
                return true;

            await cartRepository.ClearAsync(cartItms);

            return true;
        }

        public async Task<CartReposnse> GetMyCartAsync()
        {
            var userId = GetCurrentUserId();

            return BuildCartResponse(userId, await cartRepository.GetByUserIdAsync(userId));
        }

        public async Task<bool> RemoveCartItemAsync(int cartItemId)
        {
            var userId = GetCurrentUserId();

            var cartItem = await cartRepository.GetByIdAsync(cartItemId);

            if (cartItem is null || cartItem.UserId != userId)
                throw new NotFoundException("Cart item not found.");

            await cartRepository.DeleteAsync(cartItem);

            return true;
        }

        public async Task<CartReposnse> UpdateCartItemASync(int cartItemId, UpdateCartItemRequest request)
        {
            var userId = GetCurrentUserId();

            var cartItem = await cartRepository.GetByIdAsync(cartItemId);

            if (cartItem is null || cartItem.UserId != userId)
                throw new NotFoundException("Cart item not found.");

            var product = await productRepository.GetByIdAsync(cartItem.ProductId)
                ?? throw new NotFoundException("Product not found.");

            if (product.StockQuantity < request.Quantity)
                throw new BadRequestException("Requested quantity exceeds available stock.");

            cartItem.Quantity = request.Quantity;

            await cartRepository.Updatesync(cartItem);

            return BuildCartResponse(userId, await cartRepository.GetByUserIdAsync(userId));
        }

        private int GetCurrentUserId()
        {
            if (!currentUserService.IsAuthenticated ||
                !currentUserService.UserId.HasValue)
            {
                throw new UnauthorizedException("User is not authenticated.");
            }

            return currentUserService.UserId.Value;
        }

        private CartReposnse BuildCartResponse(int userId,List<CartItem> cartItems)
        {
            var items = mapper.Map<List<CartItemResponse>>(cartItems);
            return new CartReposnse
            {
                UserId = userId,
                Items = items,
                TotalAmount = items.Sum(i=>i.LineTotal)
            };
        }
    }
}
