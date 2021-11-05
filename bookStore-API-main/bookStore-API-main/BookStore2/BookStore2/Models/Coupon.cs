using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace BookStore2.Models
{
    public class Coupon
    {
        public int couponId { get; set; }
        public string couponName { get; set; }
        public double couponDiscount { get; set; }

        SqlConnection con = new SqlConnection("server=IND364;database=bookstoreDB2;integrated security=true");

        SqlCommand cmd_getAllCoupon = new SqlCommand("select * from tbl_coupons");

        List<Coupon> couponAllList = new List<Coupon>();
        public List<Coupon> GetAllCoupons()
        {
            cmd_getAllCoupon.Connection = con;
            SqlDataReader _read;
            con.Open();

            _read = cmd_getAllCoupon.ExecuteReader();

            while (_read.Read())
            {
                couponAllList.Add(new Coupon()
                {

                    couponId = Convert.ToInt32(_read[0]),
                    couponName = _read[1].ToString(),
                    couponDiscount = Convert.ToDouble(_read[2])


                });
            }
            _read.Close();
            con.Close();
            return couponAllList;
        }

    }
}