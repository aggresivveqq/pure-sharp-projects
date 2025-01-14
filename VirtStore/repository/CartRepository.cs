using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtStore.interfaces;
using VirtStore.models;

namespace VirtStore.repository
{
    public class CartRepository : IRepository<Cart>
    {
        private const string FilePath = "Cart.json";
        public List<Cart> GetAll()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Cart>>(json);
            }
            return new List<Cart>();
        }
        public void Save(List<Cart> carts)
        {
            var jsonData = JsonConvert.SerializeObject(carts, Formatting.Indented);
            File.WriteAllText(FilePath, jsonData);
        }
        public Cart GetById(int id)
        {
            var carts = GetAll();
            return  carts.FirstOrDefault(c => c.Id == id);
        }
        public void Add(Cart cart)
        {
            var carts = GetAll();
            carts.Add(cart);
            Save(carts);
        }
        public void Update(Cart cart) {
            var carts = GetAll();
            var existingCart = carts.FirstOrDefault(cart => cart.Id == cart.Id);
            if (existingCart != null) {
                existingCart.Products = cart.Products;
                existingCart.TotalPrice = cart.TotalPrice;
               Save(carts);
            }
        
        }
        public void Delete(int id)
        {
            var carts = GetAll(); 
            var cartToRemove = carts.FirstOrDefault(c => c.Id == id);

            if (cartToRemove != null)
            {
                carts.Remove(cartToRemove);
                Save(carts);
            }
        }

    }
}
