using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // Lista simulada de productos (en memoria, solo para el ejemplo)
        private static List<string> _products = new() { "Laptop", "Mouse", "Teclado" };

        [HttpGet("{id}")]
        public ActionResult<string> GetById(int id)
        {
            // Validamos si el id existe en la lista
            if (id < 0 || id >= _products.Count)
            {
                return NotFound("Producto no encontrado..."); // 404 - no hay data que devolver
            }

            string product = _products[id];

            return product; // 200 - devuelve el string directo, sin usar Ok()
        }

        [HttpGet("search")]
        public ActionResult<string> GetByQuery([FromQuery] string name)
        {
            return name;
        }
    }
}
