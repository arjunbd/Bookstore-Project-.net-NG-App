using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace BookStore2.Models
{
    public class BookOrders
    {
        public int orderId { get; set; }
        public int bookId { get; set; }
        public int cartId { get; set; }
        public string couponId { get; set; } // string data type because the couponId from database can be NULL
        public double totalCost { get; set; }

        SqlConnection con = new SqlConnection("server=IND364;database=bookstoreDB2;integrated security=true");

        SqlCommand cmd_createOrder = new SqlCommand("sp_createOrder");
        SqlCommand cmd_getOrdersFromUsername = new SqlCommand("sp_getOrdersFromUsername");
        SqlCommand cmd_getTotalcost = new SqlCommand("sp_getTotal");
        SqlCommand cmd_getBookListFromOrder = new SqlCommand("sp_getBookListFromOrder");
        public List<eachBokksOrder> GetBookListFromOrder(int orderId)
        {
            List<eachBokksOrder> singleBookOrderList = new List<eachBokksOrder>();

            cmd_getBookListFromOrder.Connection = con;
            cmd_getBookListFromOrder.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_getBookListFromOrder.Parameters.AddWithValue("@orderId", orderId);
            SqlDataReader _read;
            con.Open();

            _read = cmd_getBookListFromOrder.ExecuteReader();

            while (_read.Read())
            {
                singleBookOrderList.Add(new eachBokksOrder()
                {

                    bookTitle = _read[0].ToString(),
                    orderQty = Convert.ToInt32(_read[1]),

                });
            }
            _read.Close();
            con.Close();
            return singleBookOrderList;
        }
        public double getCost(int cartId) {
            cmd_getTotalcost.Connection = con;
            cmd_getTotalcost.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_getTotalcost.Parameters.AddWithValue("@cartId", cartId);
            cmd_getTotalcost.Parameters.Add("@amount", System.Data.SqlDbType.Float);
            cmd_getTotalcost.Parameters["@amount"].Direction = System.Data.ParameterDirection.Output;
            con.Open();
            cmd_getTotalcost.ExecuteNonQuery();
            double output = Convert.ToDouble(cmd_getTotalcost.Parameters["@amount"].Value);
            con.Close();
            return output;
        }
        public int createOrder(int cartId, string couponName)
        {
            int rows_affected;

            cmd_createOrder.Connection = con;
            cmd_createOrder.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_createOrder.Parameters.AddWithValue("cartId", cartId);
            cmd_createOrder.Parameters.AddWithValue("couponName", couponName);

            con.Open();
            rows_affected = cmd_createOrder.ExecuteNonQuery();
            con.Close();

            return rows_affected;
        }

        public List<SingleUserOrder> GetOrdersFromUsername(string username)
        {
            List<SingleUserOrder> singleUserOrderList = new List<SingleUserOrder>();

            cmd_getOrdersFromUsername.Connection = con;
            cmd_getOrdersFromUsername.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_getOrdersFromUsername.Parameters.AddWithValue("username", username);

            SqlDataReader _read;
            con.Open();

            _read = cmd_getOrdersFromUsername.ExecuteReader();

            // When this loop begins we get the values from the first row in _read
            // We create an object of SingleUserOrder which will contain all the details about an order
            // We first set the orderId and totalCost for the order
            // Then we create a list to store the data about the individual book orders, singleBookOrderList
            while (_read.Read())
            {
                SingleUserOrder singleUserOrderObj = new SingleUserOrder();

                singleUserOrderObj.orderId = Convert.ToInt32(_read[0]);
                singleUserOrderObj.totalCost = Convert.ToDouble(_read[3]);

                List<SingleBookOrder> singleBookOrderList = new List<SingleBookOrder>();

                while (Convert.ToInt32(_read[0]) == singleUserOrderObj.orderId) // As long as the orderId from the row we read is the same
                {
                    SingleBookOrder singleBookOrderObj = new SingleBookOrder()
                    {
                        bookId = Convert.ToInt32(_read[1]),
                        orderQty = Convert.ToInt32(_read[2])
                    };

                    singleBookOrderList.Add(singleBookOrderObj);

                    if (!_read.Read()) // To handle the situation after we read the last row
                    {
                        break;
                    }
                }

                singleUserOrderObj.bookOrderList = singleBookOrderList; // Add the list of Book Orders to the User Order

                singleUserOrderList.Add(singleUserOrderObj); // Add the complete User Order to the User Order list
            }

            _read.Close();
            con.Close();

            return singleUserOrderList;
        }
    }
}