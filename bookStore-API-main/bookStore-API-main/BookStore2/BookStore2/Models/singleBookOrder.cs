using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class SingleBookOrder
    {
        // This class will store details about a single book order from an order.
        public int bookId { get; set; }
        public int orderQty { get; set; }
    }
}
