
using AutoMapper;
using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;
using CleanCommerce.Domain.Entities;

namespace CleanCommerce.Application.Mapping
{
    public class CartMapping : Profile
    {
        public CartMapping()
        {
            CreateMap<AddToCartRequest, CartItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<CartItem, CartItemResponse>()
                .ForMember(dest => dest.CartItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.LineTotal, opt => opt.MapFrom(src => src.Product.Price*src.Quantity));
        }
    }
}
