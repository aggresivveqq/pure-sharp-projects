
using Store.Interfaces;
using Store.Models;

namespace Store.ui
{
    public class AdminPanel
    {
        public IUserRepository UserRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public ICartRepository CartRepository { get; private set; }

        public AdminPanel(IUserRepository userRepository, IProductRepository productRepository, IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            UserRepository = userRepository;
            ProductRepository = productRepository;
            OrderRepository = orderRepository;
            CartRepository = cartRepository;
     
        }
        public void EditorMenu()
        {
            Console.WriteLine("Press password for Editor");
            var checker = Console.ReadLine();
            if (checker != "1234") // pseudo authorization
            {
                Console.WriteLine("Wrong password");
                return;
                    
            }
            Console.WriteLine("Welcome to the editor menu!.");
            Console.WriteLine("1.Add product");
            Console.WriteLine("2.Delete product");
            Console.WriteLine("3.Update product");
            Console.WriteLine("4.Show products");
            Console.WriteLine("5.Show users");
            Console.WriteLine("6.Change users Order status.");
            Console.WriteLine("7.Delete user's order");
            Console.WriteLine("8.Exit");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    AddProductfromPanel();
                    break;
                case "2":
                  DeleteProductfromPanel();
                    break;
                case "3":
                  UpdateProductfromPanel();
                    break;
                case "4":
                    ShowCatalogue();
                    break;
                case "5":
                    ShowUsers();
                    break;
                case "6":
                    ChangeOrderStatus();
                    break;
                case "7":
                    DeleteAllUsersOrder();
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        public void AddProductfromPanel()
        {
            Console.WriteLine("Enter product name:");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Product name cannot be empty.");
                return;
            }

            Console.WriteLine("Enter product price:");
            if (!double.TryParse(Console.ReadLine(), out double price) || price <= 0)
            {
                Console.WriteLine("Invalid price. Please enter a positive number.");
                return;
            }

            var lastProduct = ProductRepository.GetAllProducts().OrderByDescending(p => p.Id).FirstOrDefault();
            var newId = lastProduct?.Id + 1 ?? 1;
            var product = new Product(newId, name, price);
            ProductRepository.AddProduct(product);

            Console.WriteLine("Product added successfully.");
        }


        public void DeleteProductfromPanel()
        {
            Console.WriteLine("Enter product ID");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
                return;
            }
            ProductRepository.DeleteProduct(id);
        }
        public void UpdateProductfromPanel()
        {
            Console.WriteLine("Enter product ID");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
                return;
            }
            var product = ProductRepository.GetProductById(id);
            if (product == null)
            {
                Console.WriteLine("Product not found");
                return;
            }
            Console.WriteLine("Enter new product name");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Product name cannot be empty.");
                return;
            }
            
            Console.WriteLine("Enter new price");
            if (!double.TryParse(Console.ReadLine(), out double price) || price <= 0)
            {
                Console.WriteLine("Invalid price.Please enter a positive number");
                return;
            }
                ProductRepository.UpdateProduct(id, name, price);
        }
        public void ShowCatalogue()
        {
            Console.WriteLine("Catalogue:");
            var products = ProductRepository.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name},Price:{product.Price}$");
            }
        }
        public void ShowUsers()
        {
            Console.WriteLine("User list with information:");
            var users = UserRepository.GetAllUsers();
            var orders = OrderRepository.GetAllOrders();

            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.Name}, ID: {user.Id}");
                var cart = CartRepository.GetCartsByUserId(user.Id);
                foreach (var cartItem in cart)
                {
                    Console.WriteLine($"Cart item: {cartItem.Product.Name}, Amount: {cartItem.Quantity},Total price:{CartRepository.GetUserCartTotalPrice(user.Id)}$");
                }
                var userOrders = orders.Where(o => o.UserId == user.Id).ToList();

                if (userOrders.Any())
                {
                    Console.WriteLine("Orders:");
                    foreach (var order in userOrders)
                    {
                        Console.WriteLine($"  Order ID: {order.Id}, Status: {order.Status}");
                        if (order.Items.Any())
                        {
                            Console.WriteLine("Items in this order:");
                            foreach (var item in order.Items)
                            {
                                Console.WriteLine($"Product names: {item.Product.Name}, Quantity: {item.Quantity}");
                            }
                        }
                        }
                }
                else
                {
                    Console.WriteLine("No orders found for this user.");
                }

                Console.WriteLine(); 
            }
        }
        public void ChangeOrderStatus()
        {
            Console.WriteLine("Enter user's ID");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
                return;
            }
            Console.WriteLine("Enter order ID for User");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
                return;
            }
            Console.WriteLine("0.Pending,\r\n            1.Processing,\r\n            2.Shipped,\r\n            3.Delivered,\r\n            4.Canceled");
            if (!int.TryParse(Console.ReadLine(), out int status))
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
                return;
            }
            try
            {
                OrderRepository.ChangeStatus(userId, orderId, (Status)status);
                Console.WriteLine($"{status} changed.");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot change status.");
            }
            
        }
        public void DeleteAllUsersOrder()
        {
            Console.WriteLine("Enter user's ID:");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }
            Console.WriteLine("Enter order ID to delete:");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }
            try
            {
                OrderRepository.DeleteOrderByUser(userId,orderId);
                Console.WriteLine($"Order with ID {orderId} for User {userId} has been deleted.");
            }
            catch (Exception ex) {
                Console.WriteLine("Can't delete order...");
                return;
            }
           
            
        }
    }
}
