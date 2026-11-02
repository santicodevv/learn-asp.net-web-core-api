using System;
using ApiEcommerce.Repository.IRepository;

namespace ApiEcommerce.Services;

public class CategoryServices : ICategoryServices
{
    private readonly ICategoryRepository _repository;
    public CategoryServices(ICategoryRepository categoryRepository)
    {
        _repository = categoryRepository;
    }

    public async Task<ICollection<Category>> GetCategoriesAsync()
    {
        return  await _repository.GetCategoriesAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _repository.GetCategoryByIdAsync(id);
    }

    public async Task<bool> CategoryExistsAsync(int id)
    {
        return await _repository.CategoryExistsAsync(id);
    }

    public async Task<bool> CategoryExistsAsync(string name)
    {
        return await _repository.CategoryExistsAsync(name);
    }


    public async Task<bool> CreateCategoryAsync(Category category)
    {
        return await _repository.CreateCategoryAsync(category);
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        return await _repository.UpdateCategoryAsync(category);
    }

    public async Task<bool> DeleteCategoryAsync(Category category)
    {
        return await _repository.DeleteCategoryAsync(category);
    }
}
