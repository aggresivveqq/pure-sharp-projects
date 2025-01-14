using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtStore.models;

namespace VirtStore.interfaces
{
    public interface IUserService
    {

        void Register(User user);
        User Authenticate(string email, string password);
        List<User> GetAllUsers();
        User GetUserById(int id);
        void UpdateUser(User user);
        void DeleteUser(int id);

    }
}