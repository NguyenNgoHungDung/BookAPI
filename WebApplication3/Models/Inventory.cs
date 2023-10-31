using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int BookID { get; set; }
        public int Quantity { get; set; }
    }
}