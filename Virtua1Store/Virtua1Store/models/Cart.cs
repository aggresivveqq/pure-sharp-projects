using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtStore.models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Dictionary<int, int> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}