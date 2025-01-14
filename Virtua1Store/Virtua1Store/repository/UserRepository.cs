using Newtonsoft.Json;
using VirtStore.interfaces;
using VirtStore.models;

namespace VirtStore.repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly string _filePath = "Users.json";


        public void Add(User user)
        {
            var users = GetAll();
            users.Add(user);
            Save(users);
        }
        public void Save(List<User> users)
        {
            SaveToFile(users);
        }

        public List<User> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<User>();

            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
        }


        public User GetById(int id)
        {
            var users = GetAll();
            return users.Find(user => user.Id == id);
        }


        public void Update(User updatedUser)
        {
            var users = GetAll();
            var user = users.Find(u => u.Id == updatedUser.Id);
            if (user != null)
            {
                users[users.IndexOf(user)] = updatedUser;
                Save(users);
            }
        }


        public void Delete(int id)
        {
            var users = GetAll();
            var userToRemove = users.Find(u => u.Id == id);
            if (userToRemove != null)
            {
                users.Remove(userToRemove);
                Save(users);
            }
        }

        private void SaveToFile(List<User> users)
        {
            var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
    }
}