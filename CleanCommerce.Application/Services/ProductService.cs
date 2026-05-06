
using AutoMapper;
using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;
using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Application.Interfaces.Services;
using CleanCommerce.Domain.Entities;

namespace CleanCommerce.Application.Services
{
    public class ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper
        ) : IProductService
    {
        public async Task<ProductResponse> AddAsync(CreateProductRequest request)
        {
            _ = await productRepository.GetByIdAsync(request.CategoryId) 
                ?? throw new Exception("Category not found.");

            var product = mapper.Map<Product>(request);

            product.Name = request.Name.Trim();
            product.Description = request.Description?.Trim();
            product.CreatedAt = DateTime.Now;

            return mapper.Map<ProductResponse>(await productRepository.AddAsync(product));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null) return false;
            await productRepository.DeleteAsync(product);
            return true;
        }

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            return mapper.Map<List<ProductResponse>>(await productRepository.GetAllAsync());
        }

        public async Task<List<ProductResponse>> GetByCategoryIdAsync(int categoryId)
        {
            return mapper.Map<List<ProductResponse>>(await productRepository.GetByCategoryIdAsync(categoryId));
        }

        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null) return null;
            return mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request)
        {
            var existingProduct = await productRepository.GetByIdAsync(id);

            if (existingProduct is null) return null;

            _ = await categoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new Exception("Category not found.");

            mapper.Map(request, existingProduct);

            existingProduct.Name = request.Name.Trim();
            existingProduct.Description = request.Description?.Trim();

            return mapper.Map<ProductResponse>(await productRepository.UpdateAsync(existingProduct));
        }
    }
}
