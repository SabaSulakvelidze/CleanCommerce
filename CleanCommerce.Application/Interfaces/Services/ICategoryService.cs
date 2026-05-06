

using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;

namespace CleanCommerce.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(int id);
        Task<CategoryResponse?> GetByNameAsync(string name);
        Task<CategoryResponse> AddAsync(CreateCategoryRequest request);
        Task<CategoryResponse?> UpdateAsync(int id, UpdateCategoryRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
