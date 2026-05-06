using AutoMapper;
using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;
using CleanCommerce.Application.Exceptions;
using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Application.Interfaces.Services;
using CleanCommerce.Domain.Entities;

namespace CleanCommerce.Application.Services
{
    public class CategoryService(
        ICategoryRepository categoryRepository,
        IMapper mapper
        ) : ICategoryService
    {
        public async Task<CategoryResponse> AddAsync(CreateCategoryRequest request)
        {
            var existingCategory = await categoryRepository.GetByNameAsync(request.Name.Trim());

            if (existingCategory is not null) 
                throw new BadRequestException($"Category with name: {request.Name.Trim()} already exists.");

            return mapper.Map<CategoryResponse>(await categoryRepository.AddAsync(mapper.Map<Category>(request)));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null) return false;

            await categoryRepository.DeleteAsync(category);

            return true;
        }

        public async Task<List<CategoryResponse>> GetAllAsync()
        {
            return mapper.Map<List<CategoryResponse>>(await categoryRepository.GetAllAsync());
        }

        public async Task<CategoryResponse?> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null) return null;
            return mapper.Map<CategoryResponse>(category);
        }

        public Task<CategoryResponse?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryResponse?> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category is null) return null;

            var existingCategory = await categoryRepository.GetByNameAsync(request.Name.Trim());

            if (existingCategory is not null && existingCategory.Id != id)
                throw new BadRequestException($"Category with name: {request.Name.Trim()} already exists.");

            mapper.Map(request, category);

            return mapper.Map<CategoryResponse>(await categoryRepository.UpdateAsync(category));
        }
    }
}
