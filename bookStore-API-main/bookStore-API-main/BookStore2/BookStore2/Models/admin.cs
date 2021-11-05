using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace BookStore2.Models
{
    public class admin
    {
        public string adminUserName { get; set; }
        public string adminPassword { get; set; }

        SqlConnection con = new SqlConnection("server=IND364;database=bookstoreDB2;integrated security=true");

        SqlCommand cmd_checkAdminPassword = new SqlCommand("sp_checkAdminPassword");
        SqlCommand cmd_adminActivateUser = new SqlCommand("sp_adminActivateUser");
        SqlCommand cmd_adminDeactivateUser = new SqlCommand("sp_adminDeactivateUser");
        SqlCommand cmd_adminAddCoupon = new SqlCommand("sp_adminAddCupon");

        public bool checkAdminPassword(string p_adminUserName, string p_adminPassword)
        {
            bool isValidCredentials;

            cmd_checkAdminPassword.Connection = con;
            cmd_checkAdminPassword.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_checkAdminPassword.Parameters.AddWithValue("@userName", p_adminUserName);
            cmd_checkAdminPassword.Parameters.AddWithValue("@passWord", p_adminPassword);
            cmd_checkAdminPassword.Parameters.Add("@outputstat", System.Data.SqlDbType.Bit, 1);
            cmd_checkAdminPassword.Parameters["@outputstat"].Direction = System.Data.ParameterDirection.Output;

            con.Open();
            cmd_checkAdminPassword.ExecuteNonQuery();
            isValidCredentials = Convert.ToBoolean(cmd_checkAdminPassword.Parameters["@outputstat"].Value);
            con.Close();

            return isValidCredentials;
        }

        public int AdminActivateUser(string p_activateUserName)
        {
            int rows_affected;

            cmd_adminActivateUser.Connection = con;
            cmd_adminActivateUser.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_adminActivateUser.Parameters.AddWithValue("userName", p_activateUserName);

            con.Open();
            rows_affected = cmd_adminActivateUser.ExecuteNonQuery();
            con.Close();

            return rows_affected;
        }

        public int AdminDeactivateUser(string p_deactivateUserName)
        {
            int rows_affected;

            cmd_adminDeactivateUser.Connection = con;
            cmd_adminDeactivateUser.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_adminDeactivateUser.Parameters.AddWithValue("userName", p_deactivateUserName);

            con.Open();
            rows_affected = cmd_adminDeactivateUser.ExecuteNonQuery();
            con.Close();

            return rows_affected;
        }

        public int AdminAddCoupon(string p_couponName, double p_discountRate)
        {
            int rows_affected;

            cmd_adminAddCoupon.Connection = con;
            cmd_adminAddCoupon.CommandType = System.Data.CommandType.StoredProcedure;
            cmd_adminAddCoupon.Parameters.AddWithValue("@cuponName", p_couponName);
            cmd_adminAddCoupon.Parameters.AddWithValue("@discountRate", p_discountRate);

            con.Open();
            rows_affected = cmd_adminAddCoupon.ExecuteNonQuery();
            con.Close();

            return rows_affected;
        }

        //        public Order AdminViewCustomerOrder(string p_customerName)
        //        {
        //
        //            cmd_adminAddCoupon.Connection = con;
        //            cmd_adminAddCoupon.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd_adminAddCoupon.Parameters.AddWithValue("couponName", p_couponName);
        //            cmd_adminAddCoupon.Parameters.AddWithValue("discountRate", p_discountRate);
        //
        //            con.Open();
        //            rows_affected = cmd_adminAddCoupon.ExecuteNonQuery();
        //            con.Close();
        //
        //            return rows_affected;
        //        }
    }
}