using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;
using ApiEcommerce.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<ActionResult<List<CategoryDTO>>> GetCategoriesAsync()
        {
            var categories = await _categoryServices.GetCategoriesAsync();
            var categoriesDtos = _mapper.Map<List<CategoryDTO>>(categories);

            return Ok(categoriesDtos);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryServices.GetCategoryByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDTO>(category);
            
            if (category is null)
            {
                return NotFound("Categoria no Encontrada.");
            }

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategoryAsync(CreateCategoryAsyncDTO dTO)
        {
            var isExistCategory = await _categoryServices.CategoryExistsAsync(dTO.Name);
            if ( isExistCategory == true)
            {
                return BadRequest($"La categoria {dTO.Name} ya existe!");
            }

            var category = _mapper.Map<Category>(dTO);
            await _categoryServices.CreateCategoryAsync(category); 
            var categoryDto = _mapper.Map<CategoryDTO>(category);

            return CreatedAtRoute("GetCategory", new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategoryAsync(int id, UpdateCategoryDTO updateCategoryDTO)
        {
            var categoryExists = await _categoryServices.CategoryExistsAsync(id);

            if (!categoryExists)
            {
                return NotFound($"La categoria {id} no fue encontrada");
            }

            var categoryUpdate = _mapper.Map<Category>(updateCategoryDTO);
            categoryUpdate.Id = id;
            await _categoryServices.UpdateCategoryAsync(categoryUpdate);
            var dto = _mapper.Map<CategoryDTO>(categoryUpdate);

            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            var existsCategory = await _categoryServices.CategoryExistsAsync(id);
            if (!existsCategory)
            {
                return NotFound($"La categoria {id} no existe");
            }

            var category = await _categoryServices.GetCategoryByIdAsync(id);

            if (category is null)
            {
                return NotFound($"La categoria {id} no fue encontrada");
            }

            var deleteCategory = await _categoryServices.DeleteCategoryAsync(category);
            if (!deleteCategory)
            {
                return StatusCode(500, $"Algo salio mal al eliminar el registro {category.Name}");
            }

            return NoContent();
        }
    }
}
