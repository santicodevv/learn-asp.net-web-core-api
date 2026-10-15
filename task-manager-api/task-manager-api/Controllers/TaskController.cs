using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{

    private readonly ITaskService _taskService;
    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskDto>>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var result = await _taskService.GetAllAsync(page, pageSize);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskDto>> GetByIdAsync(int id)
    {
        var findTask = await _taskService.GetByIdAsync(id);

        if (findTask is null)
            return NotFound("Task not found");

        return Ok(findTask);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateAsync(CreateTaskDto dto)
    {
        var finishTask = await _taskService.CreateAsync(dto);
        
        return CreatedAtAction(nameof(GetByIdAsync), new { Id = finishTask.Id }, finishTask);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskDto>> UpdateAsync(int id, UpdateTaskDto updateTaskDto)
    {
        var task = await _taskService.UpdateAsync(id, updateTaskDto);

        if (task is null)
            return NotFound("Task not found");

        return Ok(task);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<TaskDto>>> GetByStateAsync(bool state)
    {
        var result = await _taskService.GetByStateAsync(state);

        if (result.Count == 0)
            return NotFound();
        
        return Ok(result);
    }

}