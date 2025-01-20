using Store.Interfaces;
using Store.Models;

namespace Store.ui
{
    public class Menu
    {
        public IUserRepository UserRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public ICartRepository CartRepository { get; private set; }
        public AdminPanel AdminPanel { get; private set; }
        public Menu(IUserRepository userRepository, IProductRepository productRepository, IOrderRepository orderRepository, ICartRepository cartRepository, AdminPanel adminPanel)
        {
            UserRepository = userRepository;
            ProductRepository = productRepository;
            OrderRepository = orderRepository;
            CartRepository = cartRepository;
            AdminPanel = adminPanel;
        }
        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. View products");
                Console.WriteLine("4. Login as editor.");
                Console.WriteLine("Choose option:");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        ShowCatalogue();
                        break;
                    case "4":
                        AdminPanel.EditorMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
        public void Register()
        {
            Console.WriteLine("Enter a username:");
            var username = Console.ReadLine();
            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Username cannot be empty.");
                return;
            }

            Console.WriteLine("Enter a password:");
            var password = Console.ReadLine();
            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Password cannot be empty.");
                return;
            }
            var lastUser = UserRepository.GetAllUsers()
                                         .OrderByDescending(u => u.Id)
                                         .FirstOrDefault();

            var newId = lastUser == null ? 1 : lastUser.Id + 1;
            var newUser = new User(newId, username, password);
            UserRepository.AddUser(newUser);
            Console.WriteLine("Registration completed.");
        }
        public void Login()
        {
            Console.WriteLine("Enter your username:");
            var username = Console.ReadLine();

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Username cannot be empty.");
                return;
            }

            var user = UserRepository.GetAllUsers().FirstOrDefault(u => u.Name == username);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("Enter your password:");
            var password = Console.ReadLine();

            if (user.Password == password && user.Name == username)
            {
                Console.WriteLine("Login successful.");
                LoginMenu(username);
            }
            else
            {
                Console.WriteLine("Invalid password.");
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }

        }
        public void ShowCatalogue()
        {
            Console.WriteLine("Catalogue:");
            var products = ProductRepository.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name},Price: {product.Price}$");
            }

        }
        public void ShowOrders(int userId)
        {
            Console.WriteLine("Your orders:");

            var orders = OrderRepository.GetOrdersByUserId(userId);

            if (orders == null || !orders.Any())
            {
                Console.WriteLine("No orders found.");
                return;
            }

            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Status: {order.Status}");

                if (order.Items.Any())
                {
                    Console.WriteLine("Items in this order:");
                    foreach (var item in order.Items)
                    {
                        Console.WriteLine($"Product names: {item.Product.Name}, Quantity: {item.Quantity}");
                    }
                }
                else
                {
                    Console.WriteLine("No items in this order.");
                }
            }
        }
        public void ShowCart(int userId)
        {
            Console.WriteLine("Cart:");
            var carts = CartRepository.GetCartsByUserId(userId);
            foreach (var cart in carts)
            {
                Console.WriteLine($"Product ID: {cart.Product.Id} Total Items: {cart.Product.Name}, Total count: {cart.Quantity},Total price: {CartRepository.GetUserCartTotalPrice(userId)}$");
            }
        }
        public void LoginMenu(string username)
        {
            while (true)
            {
                var userData = UserRepository.GetAllUsers();
                var filteredUser = userData.Find(u => u.Name == username);
                var userCart = CartRepository.GetCartsByUserId(filteredUser.Id);

                Console.WriteLine($"Welcome,{filteredUser.Name}!");
                Console.WriteLine("1.Show cart");
                Console.WriteLine("2.Add product to cart");
                Console.WriteLine("3.Delete product from cart");
                Console.WriteLine("4.Edit amount of product");
                Console.WriteLine("5.Show catalogue");
                Console.WriteLine("6.Show orders.");
                Console.WriteLine("7.Confirm order.");
                Console.WriteLine("8.Cancel order.");
                Console.WriteLine("9.Exit");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":

                        if (userCart == null)
                        {
                            Console.WriteLine("Cart is empty or not found.");
                            break;
                        }
                        else
                        {
                            ShowCart(filteredUser.Id);
                        }

                        break;
                    case "2":
                        Console.WriteLine("Enter product's ID to cart:");
                        if (!int.TryParse(Console.ReadLine(), out var productId))
                        {
                            Console.WriteLine("Invalid product ID. Please enter a valid number.");
                            break;
                        }

                        var filteredProduct = ProductRepository.GetProductById(productId);
                        if (filteredProduct == null)
                        {
                            Console.WriteLine("Product not found.");
                            break;
                        }

                        Console.WriteLine("Amount of products:");
                        if (!int.TryParse(Console.ReadLine(), out var amount) || amount <= 0)
                        {
                            Console.WriteLine("Invalid amount. Please enter a valid number greater than 0.");
                            break;
                        }

                        CartRepository.AddProductToUserCart(filteredUser.Id, filteredProduct, amount);
                        Console.WriteLine("Product added to cart.");
                        break;
                    case "3":
                        Console.WriteLine("Enter product ID");
                        if (!int.TryParse(Console.ReadLine(), out var productIdtoDelete))
                        {
                            Console.WriteLine("Invalid product ID. Please enter a valid number.");
                            break;
                        }
                        var userCarts = CartRepository.GetCartsByUserId(filteredUser.Id);
                        var maxProductId = userCarts.Max(c => c.Product.Id);
                        if (!userCarts.Any())
                        {
                            Console.WriteLine("The user has no products in their cart.");
                            return;
                        }
                        if (productIdtoDelete > maxProductId)
                        {
                            Console.WriteLine("No such id");
                            return;
                        }

                        CartRepository.RemoveProductFromUserCart(filteredUser.Id, productIdtoDelete);
                        Console.WriteLine("Successfully deleted.");
                        break;
                    case "4":
                        Console.WriteLine("Product ID to Edit");
                        if (!int.TryParse(Console.ReadLine(), out var productIdtoEdit))
                        {
                            Console.WriteLine("Invalid product ID. Please enter a valid number.");
                            break;
                        }
                        Console.WriteLine("Amount to edit");
                        if (!int.TryParse(Console.ReadLine(), out var editAmount))
                        {
                            Console.WriteLine("Invalid product ID. Please enter a valid number.");
                            break;
                        }
                        var userCartstoEdit = CartRepository.GetCartsByUserId(filteredUser.Id);
                        var maxProductIdtoEdit = userCartstoEdit.Max(c => c.Product.Id);
                        if (!userCartstoEdit.Any())
                        {
                            Console.WriteLine("The user has no products in their cart.");
                            return;
                        }
                        if (productIdtoEdit > maxProductIdtoEdit)
                        {
                            Console.WriteLine("No such id");
                            return;
                        }
                        CartRepository.UpdateProductQuantityInUserCart(filteredUser.Id, productIdtoEdit, editAmount);
                        Console.WriteLine("Successfully edited.");
                        return;

                    case "5":
                        Console.WriteLine("Catalogue:");
                        ShowCatalogue();
                        break;

                    case "6":
                        ShowOrders(filteredUser.Id);

                        break;
                    case "7":
                        Console.WriteLine("Are you sure for confirm?");
                        ;
                        Console.WriteLine("1=Yes/Other number to return.");
                        if (!int.TryParse(Console.ReadLine(), out var confirmChoice))
                        {
                            Console.WriteLine("Invalid . Please enter a valid number.");
                            break;
                        }
                        if (confirmChoice == 1)
                        {
                            var cart = CartRepository.GetCartsByUserId(filteredUser.Id);
                            OrderRepository.AddOrder(filteredUser.Id, cart);
                            Console.WriteLine("Order created");
                        }
                       
                        break;
                    case "8":
                        Console.WriteLine("Enter OrderId to cancel");
                        ShowOrders(filteredUser.Id);  
                        if (!int.TryParse(Console.ReadLine(), out var deleteChoice))  
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                            break;
                        }

                        try
                        {
                            OrderRepository.DeleteOrderById(deleteChoice); 
                            Console.WriteLine("Order successfully canceled.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

    }
}
