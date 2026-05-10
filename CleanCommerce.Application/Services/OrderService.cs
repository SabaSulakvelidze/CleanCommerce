
using AutoMapper;
using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;
using CleanCommerce.Application.Exceptions;
using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Application.Interfaces.Security;
using CleanCommerce.Application.Interfaces.Services;
using CleanCommerce.Domain.Entities;
using CleanCommerce.Domain.Enums;
using System.Net;

namespace CleanCommerce.Application.Services
{
    public class OrderService(
        IOrderRepository orderRepository,
        ICartRepository cartRepository,
        IProductRepository productRepository,
        ICurrentUserService currentUserService,
        IMapper mapper
        ) : IOrderService
    {
        public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request)
        {
            var userId = GetCurrentUserId();

            var cartItems = await cartRepository.GetByUserIdAsync(userId) ??
                throw new BadRequestException("Cart is empty.");

            var ProductsToUpdate = new List<Product>();

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Pending,
                OrderItems = []
            };

            decimal totalAmount = 0;

            foreach (var cartItem in cartItems)
            {
                var product = await productRepository.GetByIdAsync(cartItem.ProductId)
                    ?? throw new NotFoundException($"Product with id {cartItem.ProductId} was not found");

                if (product.StockQuantity < cartItem.Quantity)
                    throw new BadRequestException($"Not enaugh quantity in stock for product '${product.Name}'");

                product.StockQuantity -= cartItem.Quantity;
                ProductsToUpdate.Add(product);

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = cartItem.Quantity,
                    UnitPrice = product.Price
                });

                totalAmount = +product.Price * cartItem.Quantity;

            }

            order.TotalAmount = totalAmount;

            await productRepository.UpdateRangeAsync(ProductsToUpdate);

            var createdOrder = await orderRepository.AddSync(order);

            await cartRepository.ClearAsync(cartItems);
            return mapper.Map<OrderResponse>(createdOrder);
        }

        public async Task<List<OrderListItemResponse>> GetAllOrderAsync()
        {
            return mapper.Map<List<OrderListItemResponse>>(await orderRepository.GetAllAsync());
        }

        public async Task<List<OrderListItemResponse>> GetMyOrderAsync()
        {
            return mapper.Map<List<OrderListItemResponse>>(await orderRepository.GetByUserIdAsync(GetCurrentUserId()));
        }

        public async Task<OrderResponse?> GetMyOrderByIdAsync(int orderId)
        {
            var userId = GetCurrentUserId();

            var order = await orderRepository.GetByIdAsyunc(orderId);

            if (order is null || order.UserId != userId)
                throw new BadRequestException("Order was not found.");
            return mapper.Map<OrderResponse>(order);
        }

        public async Task<OrderResponse?> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusRequest request)
        {
            var order = await orderRepository.GetByIdAsyunc(orderId)
                ?? throw new NotFoundException("Order was not found.");

            order.Status = request.Status;

            return mapper.Map<OrderResponse>(await orderRepository.UpdateAsync(order));
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
    }
}
