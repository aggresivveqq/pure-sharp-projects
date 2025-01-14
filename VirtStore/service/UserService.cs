using System;
using System.Collections.Generic;
using VirtStore.interfaces;
using VirtStore.models;
using VirtStore.repository;
namespace VirtStore.service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public void Register(User user) {
            var existingUser = _userRepository.GetById(user.Id);
            if (existingUser != null)
            {
                throw new Exception("ID is not exist.");
            }
                _userRepository.Add(user);
        }
        public User Authenticate(string email, string password)
        {
            var users = _userRepository.GetAll();
            var user = users.Find(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                throw new Exception("Invalid email and password");
            }

            return user;
        }
        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
            
        }
        public User GetUserById(int id) {
            return _userRepository.GetById(id);
        }
        public void UpdateUser(User user)
        {
            var existingUser = _userRepository.GetById(user.Id);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            var existingUser = _userRepository.GetById(id);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            _userRepository.Delete(id);
        }
    }
}
