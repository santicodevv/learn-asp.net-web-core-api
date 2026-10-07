public class TaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new List<TaskItem>();

    public List<TaskItem> GetAll(int page, int pageSize)
    {
        var result = _tasks.Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

        return result;
    }

    public TaskItem? GetById(int id)
    {
        var result = _tasks.FirstOrDefault(t => t.Id == id);
        return result;
    }

    public void Add(TaskItem task)
    {
        _tasks.Add(task);
    }

    public List<TaskItem> GetByState(bool state)
    {
        var result = _tasks.Where(t => t.IsCompleted == state)
        .ToList();
        return result;
    }

    public int GenerateId()
    {
        return _tasks.Count + 1;
    }
}