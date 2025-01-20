using Store.Models;

namespace Store.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();      
        User GetUserById(int id);     
        void AddUser(User user);        
        void UpdateUser(int id, User updatedUser);
        void DeleteUser(int id);       
    }
}
