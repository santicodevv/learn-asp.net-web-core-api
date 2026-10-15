public interface ITaskService
{
    Task<List<TaskDto>> GetAllAsync(int page, int pageSize);
    Task<List<TaskDto>> GetByStateAsync(bool state);
    Task<TaskDto?> GetByIdAsync(int id);
    Task<TaskDto> CreateAsync(CreateTaskDto task);
    Task<TaskDto?> UpdateAsync(int id, UpdateTaskDto updateTaskDto);
}