using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class Users
    {
        public string userName { get; set; }
        public string userPassword { get; set; }
        public bool userStatus { get; set; }
        public string shippingAddress { get; set; }

        SqlConnection con = new SqlConnection("server=IND364;database=bookstoreDB2;integrated security=true");
        SqlCommand cmd_userRegister = new SqlCommand("sp_userRegister");
        SqlCommand cmd_onLogin = new SqlCommand("onLogin");
        SqlCommand cmd_updateShippingAdress = new SqlCommand("sp_updateShippingAddress");
        SqlCommand cmd_getUsers = new SqlCommand("select * from tbl_userDetails");
        SqlCommand cmd_getUserByName = new SqlCommand("select * from  tbl_userDetails where userName = @username");
        SqlCommand cmd_getUserDetails = new SqlCommand("sp_getUserDetails");
        List<Users> AllUsers = new List<Users>();
        public Users getUserDetails(string userName)
        {
            cmd_getUserDetails.Connection = con;
            cmd_getUserDetails.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_getUserDetails.Parameters.AddWithValue("userName", userName);

            SqlDataReader _read;

            con.Open();

            _read = cmd_getUserDetails.ExecuteReader();
            _read.Read();

            Users userObj = new Users()
            {
                userName = _read[0].ToString(),
                userStatus = Convert.ToBoolean(_read[2]),
                shippingAddress = _read[3].ToString()
            };

            _read.Close();
            con.Close();

            return userObj;
        }
        public List<Users> getallUsers() {
            cmd_getUsers.Connection = con;
            SqlDataReader _read;
            con.Open();

            _read = cmd_getUsers.ExecuteReader();

            while (_read.Read())
            {
                AllUsers.Add(new Users()
                {
                 userName =_read[0].ToString(),
                 userPassword = _read[1].ToString(),
                 userStatus = Convert.ToBoolean(_read[2]),
                 shippingAddress = _read[3].ToString(),

                });
            }
            _read.Close();
            con.Close();
            return AllUsers;
        }
        public int userRegister(Users obj) 
        {
            cmd_userRegister.Connection = con;
            cmd_userRegister.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_userRegister.Parameters.AddWithValue("@userName", obj.userName);
            cmd_userRegister.Parameters.AddWithValue("@userPassword", obj.userPassword);
            cmd_userRegister.Parameters.AddWithValue("@shippingAddress", obj.shippingAddress);
            con.Open();
            int output = cmd_userRegister.ExecuteNonQuery();
            con.Close();
            return output;
        }
        public int changeShipping(string userName, string newShippingAddress) 
        {
            cmd_updateShippingAdress.Connection = con;
            cmd_updateShippingAdress.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_updateShippingAdress.Parameters.AddWithValue("@userName",userName);
            cmd_updateShippingAdress.Parameters.AddWithValue("@newShippingAddress",newShippingAddress);
            con.Open();
            int output = cmd_updateShippingAdress.ExecuteNonQuery();
            con.Close();
            return output;
        }
        public string onLogin(loginCredentials obj) 
        {
            cmd_onLogin.Connection = con;
            cmd_onLogin.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_onLogin.Parameters.AddWithValue("@userName", obj.userName);
            cmd_onLogin.Parameters.AddWithValue("@password", obj.userPassword);
            cmd_onLogin.Parameters.Add("@outputstring", System.Data.SqlDbType.Char,20);
            cmd_onLogin.Parameters["@outputstring"].Direction = System.Data.ParameterDirection.Output;
            con.Open();
            cmd_onLogin.ExecuteNonQuery();
            string output = Convert.ToString(cmd_onLogin.Parameters["@outputstring"].Value);
            con.Close();
            return output;
        }
    }
    public class loginCredentials 
    {
        public string userName { get; set; }
        public string userPassword { get; set; }
    }
}