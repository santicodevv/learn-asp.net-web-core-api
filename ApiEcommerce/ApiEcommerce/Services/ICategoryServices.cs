using System;

namespace ApiEcommerce.Services;

public interface ICategoryServices
{
    ICollection<Category> GetCategories();
    
}
