using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly List<TaskItem> _tasks = new List<TaskItem>
    {
        new TaskItem
        {
            Id = 1,
            Title = "Learn C#",
            Description = null,
            Priority = 3,
            IsCompleted = true,
            DueDate = DateTime.Now.AddDays(15),
            CreatedAt = DateTime.Now
        }
    };

    [HttpGet]
    public ActionResult<List<TaskDto>> GetAll(int page = 1, int pageSize = 10)
    {
        var result = _tasks.Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(t => MapToDto(t))
        .ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public ActionResult<TaskDto> GetById(int id)
    {
        var findTask = _tasks.FirstOrDefault(t => t.Id == id);

        if (findTask is null)
            return NotFound("Task not found");

        var result = MapToDto(findTask);
        return Ok(result);
    }

    [HttpPost]
    public ActionResult<TaskDto> Create(CreateTaskDto dto)
    {
        var newTask = new TaskItem
        {
            Id = _tasks.Count + 1,
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = false,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            CreatedAt = DateTime.Now
        };
        _tasks.Add(newTask);

        var finishTask = MapToDto(newTask);
        
        return CreatedAtAction(nameof(GetById), new { Id = finishTask.Id }, finishTask);
    }

    [HttpPut("{id}")]
    public ActionResult<TaskDto> Update(int id, UpdateTaskDto dto)
    {
        var findTask = _tasks.FirstOrDefault(t => t.Id == id);

        if (findTask is null)
            return NotFound("Task not found");

        findTask.IsCompleted = dto.IsCompleted;

        var result = MapToDto(findTask);
        return Ok(result);
    }

    [HttpGet("search")]
    public ActionResult<List<TaskDto>> GetByState(bool state)
    {
        var result = _tasks.Where(t => t.IsCompleted == state)
        .Select(t => MapToDto(t))
        .ToList();

        if (result.Count == 0)
            return NotFound();
        
        return Ok(result);
    }

    private TaskDto MapToDto(TaskItem task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            Priority = task.Priority,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
        };
    }
}