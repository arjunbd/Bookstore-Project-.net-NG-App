using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace BookStore2.Models
{
    public class Wishlist
    {
        public string userName { get; set; }
        public string bookTitle { get; set; }

        SqlConnection con = new SqlConnection("server=IND364;database=bookstoreDB2;integrated security=true");

        SqlCommand cmd_getWishlist = new SqlCommand("sp_getWishlist");
        SqlCommand cmd_addToWishlist = new SqlCommand("sp_addToWishlist");
        SqlCommand cmd_deleteFromWishlist = new SqlCommand("sp_deleteFromWishlist");

        public List<Books> GetWishlist(string p_userName)
        {
            List<Books> userWishList = new List<Books>();

            cmd_getWishlist.Connection = con;
            cmd_getWishlist.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_getWishlist.Parameters.AddWithValue("userName", p_userName);

            SqlDataReader _read;
            con.Open();

            _read = cmd_getWishlist.ExecuteReader();

            while (_read.Read())
            {
                userWishList.Add(new Books()
                {

                    bookId = Convert.ToInt32(_read[0]),
                    categoryId = Convert.ToInt32(_read[1]),
                    bookTitle = _read[2].ToString(),
                    bookISBN = _read[3].ToString(),
                    bookYear = Convert.ToInt32(_read[4]),
                    bookPrice = Convert.ToDouble(_read[5]),
                    bookDescription = _read[6].ToString(),
                    bookPosition = _read[7].ToString(),
                    bookStatus = Convert.ToBoolean(_read[8]),
                    bookImage = _read[9].ToString(),
                    author = _read[10].ToString()

                });
            }

            _read.Close();
            con.Close();

            return userWishList;
        }

        public int AddToWishlist(Wishlist wishlistObj)
        {
            int rows_affected;

            cmd_addToWishlist.Connection = con;
            cmd_addToWishlist.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_addToWishlist.Parameters.AddWithValue("userName", wishlistObj.userName);
            cmd_addToWishlist.Parameters.AddWithValue("bookTitle", wishlistObj.bookTitle);

            con.Open();
            rows_affected = cmd_addToWishlist.ExecuteNonQuery();
            con.Close();

            return rows_affected;
        }

        public int DeleteFromWishlist(Wishlist wishlistObj)
        {
            int rows_affected;

            cmd_deleteFromWishlist.Connection = con;
            cmd_deleteFromWishlist.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_deleteFromWishlist.Parameters.AddWithValue("userName", wishlistObj.userName);
            cmd_deleteFromWishlist.Parameters.AddWithValue("bookTitle", wishlistObj.bookTitle);

            con.Open();
            rows_affected = cmd_deleteFromWishlist.ExecuteNonQuery();
            con.Close();

            return rows_affected;
        }
    }
}