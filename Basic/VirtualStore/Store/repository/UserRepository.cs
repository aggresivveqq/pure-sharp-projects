using Newtonsoft.Json;
using Store.Interfaces;
using Store.Models;

namespace Store.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _filePath = "User.json";

        public UserRepository()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            }
            catch
            {
                throw new Exception("Unable to read users.");
            }
        }

        public User GetUserById(int id)
        {
            var users = GetAllUsers();
            var user = users.FirstOrDefault(u => u.Id == id);
            return user ?? new User(0, "Unknown", "No password");
        }

        public void AddUser(User user)
        {
            var users = GetAllUsers();
           
            users.Add(user);
            SaveUsers(users);
        }

        public void UpdateUser(int id, User updatedUser)
        {
            var users = GetAllUsers();

            var userIndex = users.FindIndex(u => u.Id == id);
            if (userIndex == -1)
            {
                throw new Exception("User not found.");
            }

            users[userIndex] = updatedUser;
            SaveUsers(users);
        }

        public void DeleteUser(int id)
        {
            var users = GetAllUsers();

            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            users.Remove(user);
            SaveUsers(users);
        }

        private void SaveUsers(List<User> users)
        {
            try
            {
                string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch
            {
                throw new Exception("Error saving users.");
            }
        }
    }
}
