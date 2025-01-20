namespace Store.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public Cart(int userId, Product product, int quantity)
        {
            UserId = userId;
            Product = product;
            Quantity = quantity;
        }
    }
}
