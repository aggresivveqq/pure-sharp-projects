using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtStore.models;

namespace VirtStore.interfaces
{
    public interface IProductService
    {
        void AddProduct(Product product);

        void UpdateProduct(Product product);

       
        void DeleteProduct(int id);

        List<Product> GetAllProducts();

        Product GetProductById(int id);

    }
}
