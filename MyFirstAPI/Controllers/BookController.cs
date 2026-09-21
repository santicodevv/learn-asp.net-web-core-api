using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
                IsAvailable = false,
                Price = 15000
            },

            new Book
            {
                Id = 2,
                Title = "GitHub",
                Author = null,
                IsAvailable = false,
                Price = 1500
            },

            new Book
            {
                Id = 3,
                Title = "GitHub Actions",
                Author = null,
                IsAvailable = false,
                Price = 4000
            },

            new Book
            {
                Id = 4,
                Title = "C# Fundamentals",
                Author = "Microsoft",
                IsAvailable = true,
                Price = 30000
            },

            new Book
            {
                Id = 5,
                Title = "ASP.NET Core",
                Author = "Microsoft",
                IsAvailable = true,
                Price = 35000
            },

            new Book
            {
                Id = 6,
                Title = "Entity Framework Core",
                Author = "Microsoft",
                IsAvailable = false,
                Price = 17000
            },

            new Book
            {
                Id = 7,
                Title = "Node.js",
                Author = "Ryan Dahl",
                IsAvailable = true,
                Price = 38000
            },

            new Book
            {
                Id = 8,
                Title = "NestJS",
                Author = "Kamil Myśliwiec",
                IsAvailable = true,
                Price = 40000
            },

            new Book
            {
                Id = 9,
                Title = "TypeScript",
                Author = "Microsoft",
                IsAvailable = false,
                Price = 33000
            },

            new Book
            {
                Id = 10,
                Title = "JavaScript",
                Author = "Brendan Eich",
                IsAvailable = true,
                Price = 550000
            },

            new Book
            {
                Id = 11,
                Title = "Clean Code",
                Author = "Robert C. Martin",
                IsAvailable = false,
                Price = 10000
            },

            new Book
            {
                Id = 12,
                Title = "Design Patterns",
                Author = "Erich Gamma",
                IsAvailable = true,
                Price = 599
            },

            new Book
            {
                Id = 13,
                Title = "Docker",
                Author = "Docker Team",
                IsAvailable = true,
                Price = 3000
            },

            new Book
            {
                Id = 14,
                Title = "Kubernetes",
                Author = "Kubernetes Team",
                IsAvailable = false,
                Price = 2000
            },

            new Book
            {
                Id = 15,
                Title = "PostgreSQL",
                Author = "PostgreSQL Global Development Group",
                IsAvailable = true,
                Price = 1000
            },

            new Book
            {
                Id = 16,
                Title = "SQL Fundamentals",
                Author = null,
                IsAvailable = false,
                Price = 1500
            },

            new Book
            {
                Id = 17,
                Title = "Git",
                Author = "Linus Torvalds",
                IsAvailable = true,
                Price = 2300
            },

            new Book
            {
                Id = 18,
                Title = "Software Architecture",
                Author = "Mark Richards",
                IsAvailable = true,
                Price = 2600
            }
        };


        [HttpGet]
        public ActionResult<List<BookDto>> GetAll(int page = 1, int pageSize = 10)
        {
            var result = _books.Skip((page - 1) * pageSize)
                         .Take(pageSize)
                         .Select(b => MapToDto(b))
                         .ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<BookDto> GetById(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);

            if (book is null)
                return NotFound();

            var result = MapToDto(book);

            return Ok(result);
        }

        [HttpGet("test")]
        public ActionResult<BookDto> GetBookAvailable()
        {
            var result = _books.Where(b => b.IsAvailable == true)
                           .Select(b => MapToDto(b))
                           .ToList();

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("search")]
        public ActionResult<List<BookDto>> GetBookByAuthor(string author)
        {
            var result = _books.Where(b => b.Author?.Equals(author, StringComparison.OrdinalIgnoreCase) == true)
                            .Select(b => MapToDto(b))
                            .ToList();

            if (result.Count == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<BookDto> Create(CreateBookDto newBook)
        {
            var book = MapToModel(newBook);
            _books.Add(book);

            var dto = MapToDto(book);

            return CreatedAtAction(nameof(GetById), new { Id = dto.Id }, dto);
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

        [HttpGet("price/{minPrice:decimal}/{maxPrice:decimal}")]
        public ActionResult<List<BookDto>> GetBooksByMinMaxPrice(decimal minPrice, decimal maxPrice)
        {
            var result = _books.Where(b => b.Price >= minPrice && b.Price <= maxPrice)
                            .Select(b => MapToDto(b))
                            .ToList();  

            if (result.Count == 0)
                return NotFound();

            return Ok(result);
        }
          
        private BookDto MapToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                IsAvailable = book.IsAvailable
            };
        }

        private Book MapToModel(CreateBookDto createBookDto)
        {
            return new Book
            {
                Id = _books.Count + 1,
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                Price = createBookDto.Price,
                IsAvailable = createBookDto.IsAvailable
            };
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

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Author { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}

public class CreateBookDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = "";
    
    [MaxLength(100)]
    public string? Author { get; set; }

    [Range(0.1, 100000)]    
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}