using System;
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using AutoMapper;

namespace ApiEcommerce.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
       CreateMap<ProductDTO, Product>().ReverseMap();
       CreateMap<CreateProductDTO, Product>().ReverseMap();
       CreateMap<UpdateCategoryDTO, Product>().ReverseMap();
    }
}
