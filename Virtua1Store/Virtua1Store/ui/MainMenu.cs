using System;
using VirtStore.interfaces;
using VirtStore.models;

namespace VirtStore.UI
{
    public class MainMenu
    {
        private readonly IUserService _userService;

        public MainMenu(IUserService userService)
        {
            _userService = userService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to VirtStore");
                Console.WriteLine("1. Login as Admin");
                Console.WriteLine("2. Login as Seller");
                Console.WriteLine("3. Login as Customer");
                Console.WriteLine("4. Register");
                Console.WriteLine("5. View All Users");
                Console.WriteLine("6. Update User");
                Console.WriteLine("7. Delete User");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login("Admin");
                        break;
                    case "2":
                        Login("Seller");
                        break;
                    case "3":
                        Login("Customer");
                        break;
                    case "4":
                        Register();
                        break;
                    case "5":
                        ViewAllUsers(); 
                        break;
                    case "6":
                        UpdateUser(); 
                        break;
                    case "7":
                        DeleteUser();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Register()
        {
            Console.Clear();
            Console.Write("Enter your username: ");
            var username = Console.ReadLine();

            Console.Write("Enter your email: ");
            var email = Console.ReadLine();

            Console.Write("Enter your password: ");
            var password = Console.ReadLine();

            var user = new User
            {
                Name = username,
                Email = email,
                Password = password
            };

            try
            {
                _userService.Register(user);
                Console.WriteLine("Registration successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadKey();
        }

        private void Login(string role)
        {
            Console.Clear();
            Console.Write($"Enter your email as {role}: ");
            var email = Console.ReadLine();

            Console.Write("Enter your password: ");
            var password = Console.ReadLine();

            try
            {
                var user = _userService.Authenticate(email, password);
                Console.WriteLine($"Welcome, {user.Name}!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadKey();
        }

        private void ViewAllUsers()
        {
            Console.Clear();
            var users = _userService.GetAllUsers();

            Console.WriteLine("List of all users:");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Username: {user.Name}, Email: {user.Email}");
            }

            Console.ReadKey();
        }

        private void UpdateUser()
        {
            Console.Clear();
            Console.Write("Enter user ID to update: ");
            var userId = int.Parse(Console.ReadLine());

            var user = _userService.GetUserById(userId);

            if (user != null)
            {
                Console.Write($"Enter new username for {user.Name}: ");
                var username = Console.ReadLine();
                Console.Write($"Enter new email for {user.Email}: ");
                var email = Console.ReadLine();
                Console.Write("Enter new password: ");
                var password = Console.ReadLine();

                user.Name = username;
                user.Email = email;
                user.Password = password;

                try
                {
                    _userService.UpdateUser(user);
                    Console.WriteLine("User updated successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("User not found.");
            }

            Console.ReadKey();
        }

        private void DeleteUser()
        {
            Console.Clear();
            Console.Write("Enter user ID to delete: ");
            var userId = int.Parse(Console.ReadLine());

            try
            {
                _userService.DeleteUser(userId);
                Console.WriteLine("User deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}