using System;
using ApiEcommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;
    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<bool> CategoryExistsAsync(int id)
    {
        return await _db.Categories.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> CategoryExistsAsync(string name)
    {
        return await _db.Categories.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public async Task<bool> CreateCategoryAsync(Category category)
    {
        category.CreatedAt = DateTime.Now;
        await _db.Categories.AddAsync(category);

        return await SaveAsync();
    }

    public async Task<bool> DeleteCategoryAsync(Category category)
    {
        _db.Categories.Remove(category);
        return await SaveAsync();
    }

    public async Task<ICollection<Category>> GetCategoriesAsync()
    {
        return await _db.Categories.OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _db.Categories.FirstOrDefaultAsync(c => c.Id == id) ?? null;
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        category.CreatedAt = DateTime.Now;
        _db.Categories.Update(category);
        return await SaveAsync();
    }

        public async Task<bool> SaveAsync()
    {
        return await _db.SaveChangesAsync() >= 0 ? true : false;
    }

}