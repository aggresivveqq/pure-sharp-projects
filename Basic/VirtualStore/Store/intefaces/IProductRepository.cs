using Store.Models;

namespace Store.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(int id, string name, double price);
        void DeleteProduct(int id);



    }
}
