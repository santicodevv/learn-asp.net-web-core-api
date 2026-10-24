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

    public ICollection<Category> GetCategories()
    {
        return _repository.GetCategories();
    }
}
