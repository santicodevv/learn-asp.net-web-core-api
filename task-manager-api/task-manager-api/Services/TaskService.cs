using System.Reflection.Metadata.Ecma335;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public List<TaskDto> GetAll(int page, int pageSize)
    {
        var result = _taskRepository.GetAll(page, pageSize).Select(t => MapToDto(t)).ToList();
        return result;
    }

    public TaskDto? GetById(int id)
    {
        var findTask = _taskRepository.GetById(id);
        return findTask is null ? null : MapToDto(findTask);
    }

    public TaskDto Create(CreateTaskDto dto)
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
        _taskRepository.Add(newTask);

        return MapToDto(newTask);
    }

    public TaskDto Update(TaskDto task, UpdateTaskDto updateTaskDto)
    {
        var updateTask = new TaskItem
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = updateTaskDto.IsCompleted,
            Priority = task.Priority,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
        };
        _taskRepository.Update(updateTask);

        return MapToDto(updateTask);
    }

    public List<TaskDto> GetByState(bool state)
    {
        var result = _taskRepository.GetByState(state).Select(t => MapToDto(t)).ToList();
        return result;
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