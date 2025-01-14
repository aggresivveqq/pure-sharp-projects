using Newtonsoft.Json;
using VirtStore.interfaces;
using VirtStore.models;
namespace VirtStore.repository
{
    public class OrderRepository : IRepository<Order>
    {

        private readonly string _filePath = "Orders.json"; 

   
        public void Add(Order order)
        {
            var orders = GetAll();
            orders.Add(order); 
            Save(orders); 
        }

        public void Save(List<Order> orders)
        {
            SaveToFile(orders);  
        }
        public List<Order> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Order>();

            var jsonData = File.ReadAllText(_filePath); 
            return JsonConvert.DeserializeObject<List<Order>>(jsonData) ?? new List<Order>();
        }

        public Order GetById(int id)
        {
            var orders = GetAll();
            return orders.Find(order => order.Id == id); 
        }


        public void Update(Order updatedOrder)
        {
            var orders = GetAll();
            var order = orders.Find(o => o.Id == updatedOrder.Id);
            if (order != null)
            {
                orders[orders.IndexOf(order)] = updatedOrder;
                Save(orders); 
            }
        }

   
        public void Delete(int id)
        {
            var orders = GetAll();
            var orderToRemove = orders.Find(o => o.Id == id);
            if (orderToRemove != null)
            {
                orders.Remove(orderToRemove); 
                Save(orders); 
            }
        }
        
        private void SaveToFile(List<Order> orders)
        {
            var jsonData = JsonConvert.SerializeObject(orders, Formatting.Indented); 
            File.WriteAllText(_filePath, jsonData);
        }
        
    }

}

