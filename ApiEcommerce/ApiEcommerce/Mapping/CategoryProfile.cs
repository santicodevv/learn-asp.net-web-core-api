using ApiEcommerce.Models.Dtos;
using AutoMapper;

namespace ApiEcommerce.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryDTO, Category>().ReverseMap();
        CreateMap<CreateCategoryAsyncDTO, Category>().ReverseMap();
        CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
        
    }
}
