using Newtonsoft.Json;
using Store.Interfaces;
using Store.Models;

namespace Store.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _filePath = "Product.json";

        public ProductRepository()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
            }
            catch
            {
                throw new Exception("Unable to read products.");
            }
        }

        public Product GetProductById(int id)
        {
            var products = GetAllProducts();
            var product = products.FirstOrDefault(p => p.Id == id);
            return product ?? new Product(0, "Unknown", 0);
        }

        public void AddProduct(Product product)
        {
            var products = GetAllProducts();

            if (products.Any(p => p.Id == product.Id))
            {
                throw new Exception("Product with this ID already exists.");
            }
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;

            products.Add(product);
            SaveProducts(products);
        }

        public void UpdateProduct(int id, string name, double price)
        {
            var products = GetAllProducts();;
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Name = name;
                product.Price = price;  
                SaveProducts(products);
            }
            
        }

        public void DeleteProduct(int id)
        {
            var products = GetAllProducts();

            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            products.Remove(product);
            SaveProducts(products);
        }

        private void SaveProducts(List<Product> products)
        {
            try
            {
                string json = JsonConvert.SerializeObject(products, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch
            {
                throw new Exception("Error saving products.");
            }
        }
    }
}
