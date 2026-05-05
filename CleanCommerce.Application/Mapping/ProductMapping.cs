using AutoMapper;
using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;
using CleanCommerce.Domain.Entities;

namespace CleanCommerce.Application.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<CreateProductRequest, Product>()
                .ForMember(dest => dest.Id, opt =>opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt =>opt.Ignore())
                .ForMember(dest => dest.Category, opt =>opt.Ignore())
                .ForMember(dest => dest.CartItems, opt =>opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt =>opt.Ignore());
            CreateMap<UpdateProductRequest, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.CartItems, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());
            CreateMap<Product, ProductResponse>()
                .ForMember(dest=> dest.CategoryName, opt => opt.MapFrom(src=>src.Category.Name));
        }
    }
}
