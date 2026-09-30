public interface ITaskRepository
{
    List<TaskItem> GetAll(int page, int pageSize);
    List<TaskItem> GetByState(bool state);
    TaskItem? GetById(int id);
    void Add(TaskItem task);
    void Update(TaskItem updateTask);
    int GenerateId();
}