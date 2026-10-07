public interface ITaskService
{
    List<TaskDto> GetAll(int page, int pageSize);
    List<TaskDto> GetByState(bool state);
    TaskDto? GetById(int id);
    TaskDto Create(CreateTaskDto task);
    TaskDto? Update(int id, UpdateTaskDto updateTaskDto);
}