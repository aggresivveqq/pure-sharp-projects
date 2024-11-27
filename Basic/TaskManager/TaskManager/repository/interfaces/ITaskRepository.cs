using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.model;

namespace TaskManager.data.interfaces
{
    public interface ITaskRepository
    {
        void Add(TaskItem task);
        void Remove(int id);
        List<TaskItem> GetAll();
        void SaveChanges();
    }
}
