using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace BookStore2.Models
{
    public class Books
    {
        public int bookId { get; set; }
        public int categoryId { get; set; }
        public string bookTitle { get; set; }
        public string bookISBN { get; set; }
        public string author { get; set; }
        public int bookYear { get; set; }
        public double bookPrice { get; set; }
        public string bookDescription { get; set; }
        public string bookPosition { get; set; }
        public bool bookStatus { get; set; }
        public string bookImage { get; set; }

        SqlConnection con = new SqlConnection("server=IND364;database=bookstoreDB2;integrated security=true");
        SqlCommand cmd_getAllBooks = new SqlCommand("select * from tbl_books");
        SqlCommand cmd_getbookbyId = new SqlCommand("select * from tbl_books where bookId = @bookId");
        SqlCommand cmd_getBooksByName = new SqlCommand("sp_searchByName");
        SqlCommand cmd_getBooksByCat = new SqlCommand("sp_searchByCategory");
        SqlCommand cmd_getBooksByISBN = new SqlCommand("sp_searchByISBN");
        SqlCommand cmd_getBooksByAuthor = new SqlCommand("sp_searchByauthor");
        SqlCommand cmd_getActiveBooks = new SqlCommand("sp_activeBooks");
        SqlCommand cmd_getNewBooks = new SqlCommand("sp_newBooks");
        SqlCommand cmd_getFeaturedBooks = new SqlCommand("sp_featuredBook");
        SqlCommand cmd_adminAddBooks = new SqlCommand("sp_adminAddBooks");
        SqlCommand cmd_adminDeleteBooks = new SqlCommand("sp_adminDeleteBook");
        SqlCommand cmd_adminUpdateBook = new SqlCommand("sp_adminUpdateBook");

        List<Books> bookAllList = new List<Books>();
        List<Books> bookNameList = new List<Books>();
        List<Books> bookCatList = new List<Books>();
        //List<Books> bookISBNList = new List<Books>();
        List<Books> bookAuthorList = new List<Books>();
        List<Books> bookActiveList = new List<Books>();
        List<Books> bookNewList = new List<Books>();
        List<Books> bookFeaturedList = new List<Books>();

        public List<Books> GetAllBooks()
        {
            cmd_getAllBooks.Connection = con;
            SqlDataReader _read;
            con.Open();

            _read = cmd_getAllBooks.ExecuteReader();

            while (_read.Read())
            {
                bookAllList.Add(new Books() {

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
            return bookAllList;
        }
        public List<Books> GetBooksByName(string b_name)
        {
            cmd_getBooksByName.Connection = con;
            cmd_getBooksByName.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_getBooksByName.Parameters.AddWithValue("@Name", b_name);
            SqlDataReader _read;
            con.Open();

            _read = cmd_getBooksByName.ExecuteReader();

            while (_read.Read())
            {
                bookNameList.Add(new Books()
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
                    author = _read[10].ToString(),
                });
            }
            _read.Close();
            con.Close();
            return bookNameList;
        }
        public List<Books> GetBooksByCat(string cat_name)
        {
            cmd_getBooksByCat.Connection = con;
            cmd_getBooksByCat.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_getBooksByCat.Parameters.AddWithValue("@Category", cat_name);
            SqlDataReader _read;
            con.Open();

            _read = cmd_getBooksByCat.ExecuteReader();

            while (_read.Read())
            {
                bookCatList.Add(new Books()
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
                    author = _read[10].ToString(),
                });
            }
            _read.Close();
            con.Close();
            return bookCatList;
        }
        public Books GetBooksByISBN(string isbn)
        {
            cmd_getBooksByISBN.Connection = con;
            cmd_getBooksByISBN.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_getBooksByISBN.Parameters.AddWithValue("@ISBN", isbn);
            SqlDataReader _read;
            con.Open();

            _read = cmd_getBooksByISBN.ExecuteReader();
            _read.Read();

            Books bookDetails = new Books()
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
                author = _read[10].ToString(),
            };
            _read.Close();
            con.Close();

            return bookDetails;
        }
        public Books GetBooksById(int bookId)
        {
            cmd_getbookbyId.Connection = con;
            cmd_getbookbyId.Parameters.AddWithValue("@bookId", bookId);
            SqlDataReader _read;
            con.Open();

            _read = cmd_getbookbyId.ExecuteReader();
            _read.Read();

            Books bookDetails = new Books()
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
                author = _read[10].ToString(),
            };
            _read.Close();
            con.Close();

            return bookDetails;
        }
        public List<Books> GetBooksByAuthor(string author_name)
        {
            cmd_getBooksByAuthor.Connection = con;
            cmd_getBooksByAuthor.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_getBooksByAuthor.Parameters.AddWithValue("@author", author_name);
            SqlDataReader _read;
            con.Open();

            _read = cmd_getBooksByAuthor.ExecuteReader();

            while (_read.Read())
            {
                bookAuthorList.Add(new Books()
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
                    author = _read[10].ToString(),

                });
            }
            _read.Close();
            con.Close();
            return bookAuthorList;
        }
        public List<Books> GetActiveBooks()
        {
            cmd_getActiveBooks.Connection = con;
            cmd_getActiveBooks.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader _read;
            con.Open();

            _read = cmd_getActiveBooks.ExecuteReader();

            while (_read.Read())
            {
                bookActiveList.Add(new Books()
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
                    author = _read[10].ToString(),
                });
            }
            _read.Close();
            con.Close();
            return bookActiveList;
        }
        public List<Books> GetNewBooks()
        {
            cmd_getNewBooks.Connection = con;
            cmd_getNewBooks.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader _read;
            con.Open();

            _read = cmd_getNewBooks.ExecuteReader();

            while (_read.Read())
            {
                bookNewList.Add(new Books()
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
                    author = _read[10].ToString(),
                });
            }
            _read.Close();
            con.Close();
            return bookNewList;
        }
        public Books GetFeaturedBook()
        {
            cmd_getFeaturedBooks.Connection = con;
            cmd_getFeaturedBooks.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader _read;
            con.Open();

            _read = cmd_getFeaturedBooks.ExecuteReader();
            _read.Read();

            Books bookDetails = new Books()
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
                author = _read[10].ToString(),
            };
            _read.Close();
            con.Close();

            return bookDetails;

        }
        public int adminAddBooks(Books bookObj)
        {
            cmd_adminAddBooks.Connection = con;
            cmd_adminAddBooks.CommandType = System.Data.CommandType.StoredProcedure;

            cmd_adminAddBooks.Parameters.AddWithValue("@catid",bookObj.categoryId);
            cmd_adminAddBooks.Parameters.AddWithValue("@bookTitle", bookObj.bookTitle);
            cmd_adminAddBooks.Parameters.AddWithValue("@bookISBN", bookObj.bookISBN);
            cmd_adminAddBooks.Parameters.AddWithValue("@bookAuthor", bookObj.author);
            cmd_adminAddBooks.Parameters.AddWithValue("@bookYear", bookObj.bookYear);
            cmd_adminAddBooks.Parameters.AddWithValue("@bookPrice", bookObj.bookPrice);
            cmd_adminAddBooks.Parameters.AddWithValue("@bookDescription", bookObj.bookDescription);
            cmd_adminAddBooks.Parameters.AddWithValue("@bookPosition", bookObj.bookPosition);
            cmd_adminAddBooks.Parameters.AddWithValue("@bookStatus", bookObj.bookStatus);
            cmd_adminAddBooks.Parameters.AddWithValue("@bookImage", bookObj.bookImage);

            con.Open();
            int res = cmd_adminAddBooks.ExecuteNonQuery();
            con.Close();
            return res;

        }
        public int adminDeleteBooks(int bkId)
        {
            cmd_adminDeleteBooks.Connection = con;
            cmd_adminDeleteBooks.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_adminDeleteBooks.Parameters.AddWithValue("@bookId", bkId);

            con.Open();
            int res = cmd_adminDeleteBooks.ExecuteNonQuery();
            con.Close();

            return res;
        }

        public int adminUpdateBook(int bookId, Books udtBookObj)
        {
            cmd_adminUpdateBook.Connection = con;
            cmd_adminUpdateBook.CommandType = System.Data.CommandType.StoredProcedure;

            cmd_adminUpdateBook.Parameters.AddWithValue("@bookId", bookId);
            cmd_adminUpdateBook.Parameters.AddWithValue("@catId", udtBookObj.categoryId);
            cmd_adminUpdateBook.Parameters.AddWithValue("@bookTitle", udtBookObj.bookTitle);
            cmd_adminUpdateBook.Parameters.AddWithValue("@bookPrice", udtBookObj.bookPrice);
            cmd_adminUpdateBook.Parameters.AddWithValue("@bookDescription", udtBookObj.bookDescription);
            cmd_adminUpdateBook.Parameters.AddWithValue("@bookPosition", udtBookObj.bookPosition);
            cmd_adminUpdateBook.Parameters.AddWithValue("@bookStatus", udtBookObj.bookStatus);

            con.Open();
            int res = cmd_adminUpdateBook.ExecuteNonQuery();
            con.Close();
            return res;
        }





    }
}