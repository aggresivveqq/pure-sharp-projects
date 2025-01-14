using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtStore.models;

namespace VirtStore.interfaces
{
    public interface ICartService
    {
        void AddToCart(int userId, int productId, int quantity);

        void RemoveFromCart(int userId, int productId);

        Cart GetCartByUserId(int userId);

        double GetTotalPrice(int userId);

        void Checkout(int userId);
    }
}