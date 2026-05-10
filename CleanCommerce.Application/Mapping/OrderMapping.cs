
using AutoMapper;
using CleanCommerce.Application.DTOs.Responses;
using CleanCommerce.Domain.Entities;
using System.Net;

namespace CleanCommerce.Application.Mapping
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(dest => dest.OrderItemId, opt=>opt.MapFrom(src=>src.Id))
                .ForMember(dest => dest.ProductName, opt=>opt.MapFrom(src=>src.Product.Name))
                .ForMember(dest => dest.LineTotal, opt=>opt.MapFrom(src=>src.UnitPrice*src.Quantity));

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<Order, OrderListItemResponse>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.OrderItems.Sum(oi=>oi.Quantity)));
        }
    }
}
