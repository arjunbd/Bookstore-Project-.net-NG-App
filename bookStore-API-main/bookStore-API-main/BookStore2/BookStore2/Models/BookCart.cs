using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class BookCart
    {
        public int cartId { get; set; }
        public string userName { get; set; }
        public bool orderStatus { get; set; }

        SqlConnection con = new SqlConnection("server=IND364;database=bookstoreDB2;integrated security=true");
        SqlCommand cmd_seecart = new SqlCommand("SeeCart");
        SqlCommand cmd_getallcartobj = new SqlCommand("sp_getallinCart");
        SqlCommand cmd_deletefromcart = new SqlCommand("sp_deleteFromCart");
        SqlCommand cmd_addbooktocart = new SqlCommand("sp_addBooktoKart");
        SqlCommand cmd_addbookQuantity = new SqlCommand("sp_addQuantity");
        SqlCommand cmd_subbookQuantity = new SqlCommand("sp_subQuantity");
        List<bookcartlist> cartObj = new List<bookcartlist>();
        List<deleteObj> cartObjs = new List<deleteObj>();
        public List<deleteObj> getAllObj(int id)
        {
            cmd_getallcartobj.Connection = con;
            cmd_getallcartobj.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_getallcartobj.Parameters.AddWithValue("@cartId", id);
            SqlDataReader _read;
            con.Open();

            _read = cmd_getallcartobj.ExecuteReader();

            while (_read.Read())
            {
                cartObjs.Add(new deleteObj()
                {

                   
                    bookTitle = _read[0].ToString(),
                    bookPrice = Convert.ToDouble(_read[1]),
      
                });
            }
            _read.Close();
            con.Close();
            return cartObjs;
        }
        public int Getcart_Id(string userName)
        {
            cmd_seecart.Connection = con;
            cmd_seecart.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_seecart.Parameters.AddWithValue("@userName", userName);
            cmd_seecart.Parameters.Add("@cartId", System.Data.SqlDbType.Int);
            cmd_seecart.Parameters["@cartId"].Direction = System.Data.ParameterDirection.Output;
            con.Open();
            cmd_seecart.ExecuteNonQuery();
            int output = Convert.ToInt32(cmd_seecart.Parameters["@cartId"].Value);
            con.Close();
            return output;
        }
        public int deletefromcart(string bookName, string userName) {
            cmd_deletefromcart.Connection = con;
            cmd_deletefromcart.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_deletefromcart.Parameters.AddWithValue("@bookName", bookName);
            cmd_deletefromcart.Parameters.AddWithValue("@userName", userName);
            con.Open();
            int res = cmd_deletefromcart.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int addbooktocart(bookcartlist obj) 
        {
            cmd_addbooktocart.Connection = con;
            cmd_addbooktocart.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_addbooktocart.Parameters.AddWithValue("@bookId",obj.bookId);
            cmd_addbooktocart.Parameters.AddWithValue("orderQty",obj.orderQty);
            cmd_addbooktocart.Parameters.AddWithValue("cartId",obj.cartId);
            con.Open();
            int res = cmd_addbooktocart.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int addbookquantity(bookincart obj) 
        {
            cmd_addbookQuantity.Connection = con;
            cmd_addbookQuantity.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_addbookQuantity.Parameters.AddWithValue("@bookName",obj.bookName);
            cmd_addbookQuantity.Parameters.AddWithValue("@orderQty",obj.orderQty);
            cmd_addbookQuantity.Parameters.AddWithValue("@userName",obj.userName);
            con.Open();
            int res = cmd_addbookQuantity.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int subbookquantity(string bookName,int orderQty,string userName)
        {
            cmd_subbookQuantity.Connection = con;
            cmd_subbookQuantity.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_subbookQuantity.Parameters.AddWithValue("@bookName", bookName);
            cmd_subbookQuantity.Parameters.AddWithValue("@orderQty", orderQty);
            cmd_subbookQuantity.Parameters.AddWithValue("@userName", userName);
            con.Open();
            int res = cmd_subbookQuantity.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
    public class bookcartlist
    {
        public int bookId { get; set; }
        public int orderQty { get; set; }
        public int cartId { get; set; }
    }
    public class bookincart 
    {
        public string bookName { get; set; }
        public int orderQty { get; set; }
        public string userName { get; set; } 
    }
    public class deleteObj
    {
    public string bookTitle { get; set; }
    public double bookPrice { get; set; }
}
}

