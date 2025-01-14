using System;
using System.Collections.Generic;
using VirtStore.interfaces;
using VirtStore.models;

namespace VirtStore.services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddProduct(Product product)
        {
            var existingProduct = _productRepository.GetById(product.Id);
            if (existingProduct != null)
            {
                throw new Exception("Продукт с таким ID уже существует.");
            }

            _productRepository.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _productRepository.GetById(product.Id);
            if (existingProduct == null)
            {
                throw new Exception("Продукт не найден.");
            }

            _productRepository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            var existingProduct = _productRepository.GetById(id);
            if (existingProduct == null)
            {
                throw new Exception("Продукт не найден.");
            }

            _productRepository.Delete(id);
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> GetProductsByName(string name)
        {
            var products = _productRepository.GetAll();
            return products.FindAll(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}