using TaskManager.data;
using TaskManager.data.interfaces;

namespace TaskManager.app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            ITaskRepository repository = new JsonTaskRepository();  

            
            var taskService = new TaskService(repository); 

           
            var menuHandler = new MenuHandler(taskService);


            menuHandler.ShowMenu();
        }
    }
}
