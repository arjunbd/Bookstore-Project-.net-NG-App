using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore2.Models;

namespace BookStore2.Controllers
{
    public class BookOrdersController : ApiController
    {
        BookOrders bookOrdersObj = new BookOrders();

        // api/BookOrders?userName=John
        // Returns an order given a username
        public List<SingleUserOrder> GetBookOrder(string username)
        {
            return bookOrdersObj.GetOrdersFromUsername(username);
        }
        public List<eachBokksOrder> GetBookListFromOrder(int orderId)
        {
            return bookOrdersObj.GetBookListFromOrder(orderId);
        }
        // api/BookOrder?cartId=1&couponName=
        // Creates a book order given a cartId and a couponName
        public int GetCreateOrder(int cartId, string couponName)
        {
            return bookOrdersObj.createOrder(cartId, couponName);
        }
        public double getCost(int cartId) {
            return bookOrdersObj.getCost(cartId);
        }
    }
}