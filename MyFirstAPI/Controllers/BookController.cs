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

        [HttpGet("search")]
        public ActionResult<List<Book>> GetBookByAuthor(string author)
        {
            var result = _books.Where(b => b.Author?.Equals(author, StringComparison.OrdinalIgnoreCase) == true);

            if (result.ToList().Count == 0)
                return NotFound();

            return Ok(result.ToList());
        }

        [HttpPost]
        public ActionResult<Book> Create(Book newBook)
        {
            newBook.Id = _books.Count + 1;
            _books.Add(newBook);

            return CreatedAtAction(nameof(GetById), new { Id = newBook.Id }, newBook);
        }

        [HttpPut("{id}/price")]
        public ActionResult<Book> Update(int id, [FromBody] decimal price)
        {
            var findProduct = _books.FirstOrDefault(b => b.Id == id);
            if (findProduct is null)
                return NotFound();

            findProduct.Price = price;

            return Ok(findProduct);
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