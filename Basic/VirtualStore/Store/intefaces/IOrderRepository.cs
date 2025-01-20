using Store.Models;

namespace Store.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        IEnumerable<Order> GetOrdersByUserId(int userId);
        Order GetOrderByCartId(int cartId);
        void AddOrder(int userId, List<Cart> cartItems);
        void UpdateOrderByUser(int userId, Order updatedOrder);
        void DeleteOrderByUser(int userId,int orderId);
        void DeleteOrderById(int orderId);
        void ChangeStatus(int userId, int orderId, Status status);
    }
}
