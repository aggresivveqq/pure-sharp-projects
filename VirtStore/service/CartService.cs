using System;
using System.Collections.Generic;
using System.Linq;
using VirtStore.interfaces;
using VirtStore.models;
using VirtStore.models.enums;

namespace VirtStore.services
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;

        public CartService(IRepository<Cart> cartRepository, IRepository<Product> productRepository, IRepository<Order> orderRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public void AddToCart(int userId, int productId, int quantity)
        {
            var cart = _cartRepository.GetById(userId) ?? new Cart { Id = userId, UserId = userId, Products = new Dictionary<int, int>() };
            var product = _productRepository.GetById(productId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            if (product.Quantity < quantity)
            {
                throw new Exception("Not enough quantity.");
            }

            if (cart.Products.ContainsKey(productId))
            {
                cart.Products[productId] += quantity;
            }
            else
            {
                cart.Products.Add(productId, quantity);
            }

            var carts = _cartRepository.GetAll();
            var existingCart = carts?.FirstOrDefault(c => c.UserId == userId);
            if (existingCart != null)
            {
                existingCart.Products = cart.Products;
            }
            else
            {
                carts.Add(cart);
            }

            _cartRepository.Save(carts);
        }

        public void RemoveFromCart(int userId, int productId)
        {
            var cart = _cartRepository.GetById(userId);
            if (cart == null || !cart.Products.ContainsKey(productId))
            {
                throw new Exception("Product not found in cart");
            }
            cart.Products.Remove(productId);

            var carts = _cartRepository.GetAll();
            var existingCart = carts?.FirstOrDefault(c => c.UserId == userId);
            if (existingCart != null)
            {
                existingCart.Products = cart.Products;
            }

            _cartRepository.Save(carts);
        }

        public Dictionary<int, int> GetCart(int userId)
        {
            var cart = _cartRepository.GetById(userId);
            if (cart == null)
            {
                throw new Exception("Cart not found");
            }
            return cart.Products;
        }

        public void ClearCart(int userId)
        {
            var cart = _cartRepository.GetById(userId);
            if (cart == null)
            {
                throw new Exception("Cart not found.");
            }

            cart.Products.Clear();

            var carts = _cartRepository.GetAll();
            var existingCart = carts?.FirstOrDefault(c => c.UserId == userId);
            if (existingCart != null)
            {
                existingCart.Products = cart.Products;
            }

            _cartRepository.Save(carts);
        }

        public double GetTotalPrice(int userId)
        {
            var cart = _cartRepository.GetById(userId);
            if (cart == null)
            {
                throw new Exception("Cart not found");
            }
            double total = 0;
            foreach (var productId in cart.Products.Keys)
            {
                var product = _productRepository.GetById(productId);
                if (product != null)
                {
                    total += product.Price * cart.Products[productId];
                }
            }
            return total;
        }

        public Cart GetCartByUserId(int userId)
        {
            var cart = _cartRepository.GetById(userId);
            if (cart == null)
            {
                throw new Exception("Cart not found for the given user.");
            }
            return cart;
        }

        public void Checkout(int userId)
        {
            var cart = GetCartByUserId(userId);
            if (cart.Products.Count == 0)
            {
                throw new Exception("Your cart is empty.");
            }

            double totalPrice = 0;
            foreach (var productId in cart.Products.Keys)
            {
                var product = _productRepository.GetById(productId);
                if (product == null)
                {
                    throw new Exception("Product not found.");
                }

                if (product.Quantity < cart.Products[productId])
                {
                    throw new Exception($"Not enough stock for product {product.Name}.");
                }

                totalPrice += product.Price * cart.Products[productId];
            }

            var order = new Order
            {
                UserId = userId,
                Products = new List<Product>(),
                TotalPrice = totalPrice,
                Status = Status.Pending
            };

            foreach (var productId in cart.Products.Keys)
            {
                var product = _productRepository.GetById(productId);
                for (int i = 0; i < cart.Products[productId]; i++)
                {
                    order.Products.Add(product);
                }

                product.Quantity -= cart.Products[productId];
                _productRepository.Save(_productRepository.GetAll());
            }

            var orders = _orderRepository.GetAll() ?? new List<Order>();
            orders.Add(order);
            _orderRepository.Save(orders);

            ClearCart(userId);
        }
    }
}
