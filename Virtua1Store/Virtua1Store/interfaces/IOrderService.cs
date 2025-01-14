using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtStore.models.enums;
using VirtStore.models;

namespace VirtStore.interfaces
{
    public interface IOrderService
    {
        void CreateOrder(int userId, List<int> productIds, List<int> quantities);

        List<Order> GetOrdersByUserId(int userId);

        Order GetOrderById(int orderId);

        void UpdateOrderStatus(int orderId, Status status);

        List<Order> GetAllOrders();


    }
}