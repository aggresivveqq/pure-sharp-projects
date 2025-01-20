using Newtonsoft.Json;
using Store.Interfaces;
using Store.Models;

namespace Store.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly string _filePath = "Cart.json";

        public CartRepository()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        private List<Cart> GetAllCarts()
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<Cart>>(json) ?? new List<Cart>();
            }
            catch
            {
                throw new Exception("Cannot retrieve carts.");
            }
        }

        private void SaveCarts(List<Cart> carts)
        {
            try
            {
                string json = JsonConvert.SerializeObject(carts, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch
            {
                throw new Exception("Error saving cart.");
            }
        }

        public List<Cart> GetCartsByUserId(int userId)
        {
            var carts = GetAllCarts();
            return carts.Where(c => c.UserId == userId).ToList();
        }

        public Cart GetProductFromUserCart(int userId, int productId)
        {
            var carts = GetCartsByUserId(userId);
            var cart = carts.FirstOrDefault(c => c.Product.Id == productId);
            if (cart != null)
            {
                return cart;
            }
            throw new Exception("Product not found in user's cart.");
        }

        public void AddProductToUserCart(int userId, Product product, int quantity)
        {
            var carts = GetAllCarts();
            var userCarts = carts.Where(c => c.UserId == userId).ToList();
            var existingCart = userCarts.FirstOrDefault(c => c.Product.Id == product.Id);
            if (existingCart != null)
            {
                existingCart.Quantity += quantity;
            }
            else
            {
                carts.Add(new Cart(userId, product, quantity));
            }
            SaveCarts(carts);
        }

        public void UpdateProductQuantityInUserCart(int userId, int productId, int newQuantity)
        {
            var carts = GetAllCarts();
            var cart = carts.FirstOrDefault(c => c.UserId == userId && c.Product.Id == productId);
            if (cart != null)
            {
                if (newQuantity <= 0)
                {
                    carts.Remove(cart);
                }
                else
                {
                    cart.Quantity = newQuantity;
                }
            }
            else
            {
                throw new Exception("Product not found in user's cart.");
            }
            SaveCarts(carts);
        }

        public void RemoveProductFromUserCart(int userId, int productId)
        {
            var carts = GetAllCarts();
            var cart = carts.FirstOrDefault(c => c.UserId == userId && c.Product.Id == productId);
            if (cart != null)
            {
                
                    var result = carts.Remove(cart);
             

            }
            else
            {
                throw new Exception("Product not found in user's cart.");
            }
            SaveCarts(carts);
        }

        public void ClearUserCart(int userId)
        {
            var carts = GetAllCarts();
            carts.RemoveAll(c => c.UserId == userId);
            SaveCarts(carts);
        }

        public int GetUserCartTotalQuantity(int userId)
        {
            var carts = GetCartsByUserId(userId);
            return carts.Sum(c => c.Quantity);
        }

        public double  GetUserCartTotalPrice(int userId)
        {
            var carts = GetCartsByUserId(userId);
            return carts.Sum(c => c.Product.Price * c.Quantity);
        }
    }
}
