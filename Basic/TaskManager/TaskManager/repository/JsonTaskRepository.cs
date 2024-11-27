using Newtonsoft.Json;
using TaskManager.data.interfaces;
using TaskManager.model;

namespace TaskManager.data
{
    internal class JsonTaskRepository : ITaskRepository
    {
        private readonly string _filePath;
        private List<TaskItem> _tasks; 

        public JsonTaskRepository()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "tasks.json");
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)); 
            _tasks = LoadTasks();
            Console.WriteLine($"File path: {_filePath}");

        }

        private List<TaskItem> LoadTasks() 
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<TaskItem>>(json) ?? new List<TaskItem>();  
            }

            return new List<TaskItem>();
        }

        public void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(_tasks, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public void Add(TaskItem task)  
        {
            _tasks.Add(task);
            SaveChanges();
        }

        public void Remove(int id)
        {
            _tasks.RemoveAll(x => x.Id == id);
            SaveChanges();
        }

        public List<TaskItem> GetAll() 
        {
            return _tasks;

        }
    }
}
