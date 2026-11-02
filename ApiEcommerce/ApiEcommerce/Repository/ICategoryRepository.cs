namespace ApiEcommerce.Repository.IRepository;

public interface ICategoryRepository
{
    Task<ICollection<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<bool> CategoryExistsAsync(int id);
    Task<bool> CategoryExistsAsync(string name);
    Task<bool> CreateCategoryAsync(Category category);
    Task<bool> UpdateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(Category category);
    Task<bool> SaveAsync();
}