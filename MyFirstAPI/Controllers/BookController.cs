using Microsoft.AspNetCore.Mvc;

namespace MyFirstAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly List<Book> _books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "React.JS",
                Author = "WelinDev",
                IsAvailable = false
            },

            new Book
            {
                Id = 2,
                Title = "Github",
                Author = null,
                IsAvailable = false
            },
        };

        [HttpGet]
        public ActionResult<List<Book>> GetAll()
        {
            return Ok(_books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);

            if (book is null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet("test")]
        public ActionResult<Book> GetBookAvailable()
        {
            var result = _books.Where(b => b.IsAvailable == true);

            if (result.ToList().Count == 0)
            {
                return NotFound();
            }

            return Ok(result.ToList());
        }
    }
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Author { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
};