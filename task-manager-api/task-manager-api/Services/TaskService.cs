using System.Reflection.Metadata.Ecma335;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<List<TaskDto>> GetAllAsync(int page, int pageSize)
    {
        var res = await _taskRepository.GetAllAsync(page, pageSize);

        var data = res.Select(t => MapToDto(t)).ToList();
        return data;
    }

    public async Task<TaskDto?> GetByIdAsync(int id)
    {
        var findTask = await _taskRepository.GetByIdAsync(id);
        return findTask is null ? null : MapToDto(findTask);
    }

    public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
    {
        var newTask = new TaskItem
        {
            Id = _taskRepository.GenerateId(),
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = false,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            CreatedAt = DateTime.Now
        };
        await _taskRepository.AddAsync(newTask);

        return MapToDto(newTask);
    }

    public async Task<TaskDto?> UpdateAsync(int id, UpdateTaskDto updateTaskDto)
    {
        var task = await _taskRepository.GetByIdAsync(id);

        if (task is null)
        {
            return null;
        }

        task.IsCompleted = updateTaskDto.IsCompleted;
        return MapToDto(task);
    }

    public async Task<List<TaskDto>> GetByStateAsync(bool state)
    {
        var res  = await _taskRepository.GetByStateAsync(state);
        var data = res.Select(t => MapToDto(t)).ToList();
        return data;
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