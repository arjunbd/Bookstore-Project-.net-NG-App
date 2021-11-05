using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class SingleUserOrder
    {
        // This class will store the details about a user's order
        // Stores the individual book order details as a list
        public int orderId { get; set; }
        public List<SingleBookOrder> bookOrderList { get; set; }
        public double totalCost { get; set; }
    }
}