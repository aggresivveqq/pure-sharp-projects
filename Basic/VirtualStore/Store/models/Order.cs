namespace Store.Models
    {
        public class Order
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public List<Cart> Items { get; set; }
            public Status Status { get; set; }

            public Order()
            {
                Items = new List<Cart>();
            }

            public Order(int userId, List<Cart> items)
            {
                UserId = userId;
                Items = items;
                Status = Status.Pending; 
            }
        }

        public enum Status
        {
            Pending,
            Processing,
            Shipped,
            Delivered,
            Canceled
        }
    }


