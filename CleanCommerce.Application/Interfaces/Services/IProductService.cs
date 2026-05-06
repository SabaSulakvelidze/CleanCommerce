using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;
using CleanCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCommerce.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllAsync();
        Task<ProductResponse?> GetByIdAsync(int id);
        Task<List<ProductResponse>> GetByCategoryIdAsync(int categoryId);
        Task<ProductResponse> AddAsync(CreateProductRequest request);
        Task<ProductResponse?> UpdateAsync(int id,UpdateProductRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
