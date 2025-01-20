
using Store.Models;
namespace Store.Interfaces
{
    public interface ICartRepository
    {
        List<Cart> GetCartsByUserId(int userId);
        Cart GetProductFromUserCart(int userId, int productId);
        void AddProductToUserCart(int userId, Product product, int quantity);
        void UpdateProductQuantityInUserCart(int userId, int productId, int newQuantity);
        void RemoveProductFromUserCart(int userId, int productId);
        void ClearUserCart(int userId);
        int GetUserCartTotalQuantity(int userId);
        double GetUserCartTotalPrice(int userId);
    }
}
