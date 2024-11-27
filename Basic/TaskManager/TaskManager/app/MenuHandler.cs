using TaskManager.model.Enums;
using TaskManager.model;

public class MenuHandler
{
    private readonly TaskService _service;
    public MenuHandler(TaskService service)
    {
        _service = service;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("Task Manager Menu:");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View All Tasks");
            Console.WriteLine("3. Remove Task");
            Console.WriteLine("4. Change Task Progress");
            Console.WriteLine("0. Exit");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewAllTasks();
                    break;
                case "3":
                    RemoveTask();
                    break;
                case "4":
                    UpdateTaskProgress();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    private void AddTask()
    {
        Console.Write("Enter task title: ");
        var title = Console.ReadLine();
        if (String.IsNullOrEmpty(title))
        {
            title = "Empty title.";
        }

        Console.Write("Enter task description: ");
        var description = Console.ReadLine();
        if (String.IsNullOrEmpty(description))
        {
            description = "Empty description.";  
        }

        Console.Write("Enter due time in days: ");
        int dueDays;
        bool isValid = int.TryParse(Console.ReadLine(), out dueDays);

        if (isValid)
        {
            Console.WriteLine($"Due days: {dueDays}");
        }
        else
        {
            Console.WriteLine("Invalid input for due days.");
        }

        Console.WriteLine("Enter task progress (Done, InProgress, Undone): ");
        string progressInput = Console.ReadLine();
        TaskProgress taskProgress;

        if (!Enum.TryParse(progressInput, true, out taskProgress))
        {
            Console.WriteLine("Invalid task progress input. Defaulting to 'Undone'.");
            taskProgress = TaskProgress.Undone;
        }

        var tasks = _service.GetAllTasks();
        var newId = tasks.Count > 0 ? tasks.Max(task => task.Id) + 1 : 1;

        var task = new TaskItem
        {
            Id = newId,
            Title = title,
            Description = description,
            createdAt = DateTime.Now,
            updatedAt = DateTime.Now,
            DueTime = DateTime.Now.AddDays(dueDays),
            Status = taskProgress
        };
        _service.AddTask(task);
        Console.WriteLine("Task added successfully!");
    }

    private void ViewAllTasks()
    {
        var tasks = _service.GetAllTasks();
        if (tasks.Count == 0) { 
        Console.WriteLine("No tasks available.");
        }
        foreach (var task in tasks)
        {
            Console.WriteLine($"ID: {task.Id}, Title: {task.Title}, Description: {task.Description}, Status: {task.Status},  CreatedAt:{task.createdAt},  DueDate:{task.DueTime}"); 
        }
    }

    private void RemoveTask()
    {
        Console.Write("Enter task ID to remove: ");
        var id = int.Parse(Console.ReadLine());
        _service.RemoveTask(id);
        Console.WriteLine("Task removed successfully!");
    }
    private void UpdateTaskProgress()
    {
        Console.Write("Enter task ID to update progress: ");
        var taskId = int.Parse(Console.ReadLine());

        var task = _service.GetAllTasks().FirstOrDefault(t => t.Id == taskId);

        if (task == null)
        {
            Console.WriteLine("Task not found");
            return;
        }

        Console.WriteLine("Enter new progress (Done, InProgress, Undone): ");
        string progressInput = Console.ReadLine();
        TaskProgress newStatus;

        if (Enum.TryParse(progressInput, true, out newStatus))
        {
            _service.UpdateTaskProgress(taskId, newStatus);
            Console.WriteLine("Task progress updated successfully!");
        }
        else
        {
            Console.WriteLine("Invalid progress status.");
        }
    }


}
