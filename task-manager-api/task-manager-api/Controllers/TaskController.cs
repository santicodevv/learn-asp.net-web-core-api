using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
    public ActionResult<List<TaskDto>> GetAll(int page = 1, int pageSize = 10)
    {
        var result = _taskService.GetAll(page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public ActionResult<TaskDto> GetById(int id)
    {
        var findTask = _taskService.GetById(id);

        if (findTask is null)
            return NotFound("Task not found");

        return Ok(findTask);
    }

    [HttpPost]
    public ActionResult<TaskDto> Create(CreateTaskDto dto)
    {
        var finishTask = _taskService.Create(dto);
        
        return CreatedAtAction(nameof(GetById), new { Id = finishTask.Id }, finishTask);
    }

    [HttpPut("{id}")]
    public ActionResult<TaskDto> Update(int id, UpdateTaskDto updateTaskDto)
    {
        var task = _taskService.GetById(id);

        if (task is null)
            return NotFound("Task not found");

        var taskUpdate = _taskService.Update(task, updateTaskDto);

        return Ok(taskUpdate);
    }

    [HttpGet("search")]
    public ActionResult<List<TaskDto>> GetByState(bool state)
    {
        var result = _taskService.GetByState(state);

        if (result.Count == 0)
            return NotFound();
        
        return Ok(result);
    }

}