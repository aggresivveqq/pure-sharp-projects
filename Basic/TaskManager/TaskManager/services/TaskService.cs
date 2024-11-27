using TaskManager.data.interfaces;
using TaskManager.model;
using TaskManager.model.Enums;

public class TaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public void AddTask(TaskItem task)
    {
        _repository.Add(task);
    }

    public void RemoveTask(int id)
    {
        _repository.Remove(id);
    }

    public List<TaskItem> GetAllTasks()
    {
        return _repository.GetAll();
    }
    public void UpdateTaskProgress(int taskId, TaskProgress newStatus)
    {
        var task = _repository.GetAll().FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            task.Status = newStatus;
            _repository.SaveChanges();
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }
}
