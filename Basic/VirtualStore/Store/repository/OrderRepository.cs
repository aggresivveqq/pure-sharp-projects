using Newtonsoft.Json;
using Store.Interfaces;
using Store.Models;

namespace Store.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _filePath = "Order.json";

        public OrderRepository()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<Order>>(json) ?? new List<Order>();
            }
            catch
            {
                throw new Exception("Unable to read orders.");
            }
        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            var orders = GetAllOrders();
            return orders.Where(o => o.UserId == userId);
        }

        public Order GetOrderByCartId(int cartId)
        {
            var orders = GetAllOrders();
            var order = orders.FirstOrDefault(o => o.Items.Any(i => i.Product.Id == cartId));
            return order ?? new Order();
        }

        public void AddOrder(int userId, List<Cart> cartItems)
        {
            var orders = GetAllOrders();

            var maxId = orders.Any() ? orders.Max(o => o.Id) : 0;

            var newOrder = new Order(userId, cartItems)
            {
                Id = maxId + 1  
            };



            orders.Add(newOrder);
            SaveOrders(orders);
        }

        public void UpdateOrderByUser(int userId, Order updatedOrder)
        {
            var orders = GetAllOrders();

            var orderIndex = orders.FindIndex(o => o.UserId == userId);
            if (orderIndex == -1)
            {
                throw new Exception("Order not found for the given user.");
            }

            orders[orderIndex] = updatedOrder;
            SaveOrders(orders);
        }

        public void DeleteOrderByUser(int userId,int orderId)
        {
            var orders = GetAllOrders();

            var order = orders.FirstOrDefault(o => o.UserId == userId && o.Id == orderId);

            if (order == null)
            {
                throw new Exception("Order not found for the given user and order ID.");
            }

            orders.Remove(order);
            SaveOrders(orders); 
        }
        public void DeleteOrderById(int orderId)
        {
            var orders = GetAllOrders();

            var order = orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found for the given ID.");
            }

            orders.Remove(order);
            SaveOrders(orders);
        }

        public void ChangeStatus(int userId, int orderId, Status status)
        {
            var orders = GetAllOrders();
            var order = orders.FirstOrDefault(o => o.UserId == userId && o.Id == orderId);

            if (order == null)
            {
                throw new Exception("Order not found for this user.");
            }

            order.Status = status;
            SaveOrders(orders);
        }

        private void SaveOrders(List<Order> orders)
        {
            try
            {
                string json = JsonConvert.SerializeObject(orders, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch
            {
                throw new Exception("Error saving orders.");
            }
        }
    }
}
