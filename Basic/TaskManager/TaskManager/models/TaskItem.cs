using TaskManager.model.Enums;

namespace TaskManager.model
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime DueTime { get; set; }
        public TaskProgress Status { get; set; }
    }
}
