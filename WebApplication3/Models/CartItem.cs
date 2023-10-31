using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class CartItem
    {
        public int id { get; set; }
        public int CartID { get; set; }
        public int BookID { get; set; }
        public int Quantity { get; set; }
    }
}