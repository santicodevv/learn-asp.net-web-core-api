using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>
        {
            new TaskItem
            {
                Id = 1,
                Title = "Estudiar controllers",
                Description = "Aprender las rutas y los métodos HTTP en ASP.NET Core.",
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                CompletedAt = null,
                DueDate = DateTime.Now.AddDays(2),
                Priority = 1
            },
            new TaskItem
            {
                Id = 2,
                Title = "Practicar DTOs",
                Description = "Crear un DTO y convertir un TaskItem utilizando MapToDto.",
                IsCompleted = true,
                CreatedAt = DateTime.Now.AddDays(-3),
                CompletedAt = DateTime.Now.AddDays(-1),
                DueDate = DateTime.Now,
                Priority = 2
            },
            new TaskItem
            {
                Id = 3,
                Title = "Crear endpoint GET",
                Description = "Crear un endpoint para obtener todas las tareas.",
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                CompletedAt = null,
                DueDate = DateTime.Now.AddDays(5),
                Priority = 3
            },
            new TaskItem
            {
                Id = 4,
                Title = "Aprender inyección de dependencias",
                Description = "Investigar cómo funcionan los servicios en ASP.NET Core.",
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                CompletedAt = null,
                DueDate = DateTime.Now.AddDays(7),
                Priority = 2
            }
        };


        [HttpGet("{id}")]
        public ActionResult<TaskDto> GetById(int id)
        {
            var result = _tasks.FirstOrDefault(t => t.Id == id);

            if (result is null)
                return NotFound("Task not found");

            var dto = new TaskDto
            {

            }
            return Ok(result);
        }

        [HttpGet("search")]
        public ActionResult<List<TaskDto>> SearchTasks([FromQuery] string title, [FromQuery] int? priority, [FromQuery] bool? completed, bool? overdue)
        {
            var result = _tasks.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                result = result.Where(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            }

            if (priority.HasValue)
            {
                result = result.Where(p => p.Priority.Equals(priority));
            }

            if (completed.HasValue)
            {
                result = result.Where(b => b.IsCompleted.Equals(completed));
            }

            var finalList = result.ToList();

            if (finalList.Count == 0)
                return NotFound("Tasks not found");

            return Ok(finalList);
        }

        [HttpPost]
        public ActionResult<CreateTaskDto> Create([FromBody] CreateTaskDto dto)
        {
            var newTask = new TaskItem
            {
                Id = _tasks.Count + 1,
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                CompletedAt = null,
                DueDate = DateTime.Now.AddDays(8),
                Priority = dto.Priority,
            };

            _tasks.Add(newTask);

            return CreatedAtAction(nameof(GetById), new { Id = newTask.Id }, newTask);
        }
    }
}


public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }
}

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsOverdue { get; set; }

}


public class CreateTaskDto
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }

}


public class UpdateTaskDto
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }

}