using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Book
    {
        
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public decimal Price { get; set; }
            public string ImgUrl { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public int Status { get; set; }
        

    }
}