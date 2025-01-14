using System;
using System.Collections.Generic;
using VirtStore.models;
using VirtStore.models.enums;
using VirtStore.interfaces;

namespace VirtStore.services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Cart> _cartRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<Product> productRepository, IRepository<Cart> cartRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        public void CreateOrder(int userId, List<int> productIds, List<int> quantities)
        {
            if (productIds == null || quantities == null || productIds.Count != quantities.Count)
            {
                throw new Exception("Invalid product or quantity list.");
            }

            var cart = _cartRepository.GetById(userId);
            if (cart == null || cart.Products.Count == 0)
            {
                throw new Exception("Your cart is empty.");
            }

            double totalPrice = 0;
            var order = new Order
            {
                UserId = userId,
                Status = Status.Pending,
                Products = new List<Product>()
            };

            for (int i = 0; i < productIds.Count; i++)
            {
                var product = _productRepository.GetById(productIds[i]);
                if (product == null)
                {
                    throw new Exception($"Product with ID {productIds[i]} not found.");
                }

                if (product.Quantity < quantities[i])
                {
                    throw new Exception($"Not enough stock for product {product.Name}.");
                }

                totalPrice += product.Price * quantities[i];
                product.Quantity -= quantities[i];
                _productRepository.Save(_productRepository.GetAll());

                for (int j = 0; j < quantities[i]; j++)
                {
                    order.Products.Add(product);
                }
            }

            order.TotalPrice = totalPrice;

            var orders = _orderRepository.GetAll() ?? new List<Order>();
            orders.Add(order);
            _orderRepository.Save(orders);

            // Optionally, clear the user's cart after order is created
            _cartRepository.GetById(userId)?.Products.Clear();
            _cartRepository.Save(_cartRepository.GetAll());
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return _orderRepository.GetAll().Where(order => order.UserId == userId).ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _orderRepository.GetById(orderId);
        }

        public void UpdateOrderStatus(int orderId, Status status)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            order.Status = status;
            _orderRepository.Save(_orderRepository.GetAll());
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }
    }
}