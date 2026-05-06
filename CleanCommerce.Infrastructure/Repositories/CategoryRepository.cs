
using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Domain.Entities;
using CleanCommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanCommerce.Infrastructure.Repositories
{
    public class CategoryRepository(AppDbContext context) : ICategoryRepository
    {
        public async Task<Category> AddAsync(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(Category category)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await context.Categories.FirstOrDefaultAsync(c => c.Name == name.Trim());
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return category;
        }
    }
}
