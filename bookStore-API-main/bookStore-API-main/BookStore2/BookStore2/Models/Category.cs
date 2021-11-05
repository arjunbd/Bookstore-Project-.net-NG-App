using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace BookStore2.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public string categoryDescription { get; set; }
        public string categoryImage { get; set; }
        public bool categoryStatus { get; set; }
        public string categoryPosition { get; set; }
        public string categoryCreatedAt { get; set; }

        SqlConnection con = new SqlConnection("server=IND364;database=bookstoreDB2;integrated security=true");
        SqlCommand cmd_getAllBCategory = new SqlCommand("select * from tbl_category");
        SqlCommand cmd_adminAddCategory = new SqlCommand("sp_adminAddCategory"); 
        SqlCommand cmd_adminDeleteCategory = new SqlCommand("sp_adminDeleteCategory");
        SqlCommand cmd_adminUpdateCategory = new SqlCommand("sp_adminUpdateCategory");
        SqlCommand cmd_getCategoryById = new SqlCommand("select * from tbl_category where categoryId = @categoryId");

        List<Category> categoryAllList = new List<Category>();



        public List<Category> GetAllCategory()
        {
            cmd_getAllBCategory.Connection = con;
            SqlDataReader _read;
            con.Open();

            _read = cmd_getAllBCategory.ExecuteReader();

            while (_read.Read())
            {
                categoryAllList.Add(new Category() {

                    categoryId = Convert.ToInt32(_read[0]),
                    categoryName = _read[1].ToString(),
                    categoryDescription = _read[2].ToString(),
                    categoryImage = _read[3].ToString(),
                    categoryStatus = Convert.ToBoolean(_read[4]),
                    categoryPosition = _read[5].ToString(),
                    categoryCreatedAt = _read[6].ToString()

                });
            }
            _read.Close();
            con.Close();
            return categoryAllList;
        }
        public int adminAddCategory(Category catObj)
        {
            cmd_adminAddCategory.Connection = con;
            cmd_adminAddCategory.CommandType = System.Data.CommandType.StoredProcedure;

            cmd_adminAddCategory.Parameters.AddWithValue("@categoryName", catObj.categoryName);
            cmd_adminAddCategory.Parameters.AddWithValue("@categoryDescription", catObj.categoryDescription);
            //cmd_adminAddCategory.Parameters.AddWithValue("@categoryImage", catObj.categoryImage);
            cmd_adminAddCategory.Parameters.AddWithValue("@categoryStatus", catObj.categoryStatus);
            cmd_adminAddCategory.Parameters.AddWithValue("@categoryPosition", catObj.categoryPosition);

            con.Open();
            int res = cmd_adminAddCategory.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int adminDeleteCategory(int catId)
        {
            cmd_adminDeleteCategory.Connection = con;
            cmd_adminDeleteCategory.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_adminDeleteCategory.Parameters.AddWithValue("@catId", catId);

            con.Open();
            int res = cmd_adminDeleteCategory.ExecuteNonQuery();
            con.Close();

            return res;
        }
        public int adminUpdateCategory(int catId, Category udtCatObj)
        {
            cmd_adminUpdateCategory.Connection = con;
            cmd_adminUpdateCategory.CommandType = System.Data.CommandType.StoredProcedure;

            cmd_adminUpdateCategory.Parameters.AddWithValue("@catId", catId);
            cmd_adminUpdateCategory.Parameters.AddWithValue("@categoryName", udtCatObj.categoryName);
            cmd_adminUpdateCategory.Parameters.AddWithValue("@categoryDescription", udtCatObj.categoryDescription);
            cmd_adminUpdateCategory.Parameters.AddWithValue("@categoryStatus", udtCatObj.categoryStatus);
            cmd_adminUpdateCategory.Parameters.AddWithValue("@categoryPosition", udtCatObj.categoryPosition);

            con.Open();
            int res = cmd_adminUpdateCategory.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public Category GetCategoryById(int id)        {            cmd_getCategoryById.Connection = con;            cmd_getCategoryById.Parameters.AddWithValue("@categoryId", id);            SqlDataReader _read;            con.Open();            _read = cmd_getCategoryById.ExecuteReader();            _read.Read();            Category categoryDetails = new Category()            {                categoryId = Convert.ToInt32(_read[0]),                categoryName = _read[1].ToString(),                categoryDescription = _read[2].ToString(),                categoryImage = _read[3].ToString(),                categoryStatus = Convert.ToBoolean(_read[4]),                categoryPosition = _read[5].ToString(),                categoryCreatedAt = _read[6].ToString()            };            _read.Close();            con.Close();            return categoryDetails;        }
    }
}