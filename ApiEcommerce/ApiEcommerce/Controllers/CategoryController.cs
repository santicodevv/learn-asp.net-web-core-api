using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;
using ApiEcommerce.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryServices categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<CategoryDTO>> GetCategories()
        {
            var categories = _categoryServices.GetCategories();
            var categoriesDtos = _mapper.Map<List<CategoryDTO>>(categories);

            return categoriesDtos;
        }
    }
}
