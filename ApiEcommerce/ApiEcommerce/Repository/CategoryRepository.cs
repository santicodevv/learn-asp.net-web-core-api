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

    public async Task<bool> CategoryExists(int id)
    {
        return await _db.Categories.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> CategoryExists(string name)
    {
        return await _db.Categories.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public async Task<bool> CreateCategory(Category category)
    {
        category.CreatedAt = DateTime.Now;
        await _db.Categories.AddAsync(category);

        return Save();
    }

    public async Task<bool> DeleteCategory(Category category)
    {
        _db.Categories.Remove(category);
        return Save();
    }

    public async Task<ICollection<Category>> GetCategories()
    {
        return await _db.Categories.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Category> GetCategory(int id)
    {
        return await _db.Categories.FirstOrDefaultAsync(c => c.Id == id) ?? throw new InvalidOperationException($"La categoria con el id {id} no existe");
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }

    public bool UpdateCategory(Category category)
    {
        category.CreatedAt = DateTime.Now;
        _db.Categories.Update(category);
        return Save();
    }
}