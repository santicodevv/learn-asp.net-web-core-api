public class TaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new List<TaskItem>();

    public Task<List<TaskItem>> GetAllAsync(int page, int pageSize)
    {
        var result = _tasks.Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

        return Task.FromResult(result);
    }

    public Task<TaskItem?> GetByIdAsync(int id)
    {
        var result = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(result);
    }

    public Task AddAsync(TaskItem task)
    {
        _tasks.Add(task);
        return Task.CompletedTask;
    }

    public Task<List<TaskItem>> GetByStateAsync(bool state)
    {
        var result = _tasks.Where(t => t.IsCompleted == state)
        .ToList();
        return Task.FromResult(result);
    }

    public int GenerateId()
    {
        return _tasks.Count + 1;
    }
}