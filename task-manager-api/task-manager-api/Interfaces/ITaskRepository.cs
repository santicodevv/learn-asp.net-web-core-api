public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync(int page, int pageSize);
    Task<List<TaskItem>> GetByStateAsync(bool state);
    Task<TaskItem?> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    int GenerateId();
}