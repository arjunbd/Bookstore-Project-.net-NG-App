using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore2.Models;

namespace BookStore2.Controllers
{
    public class CouponController : ApiController
    {
        Coupon couponObj = new Coupon();
        //api/coupon
        public List<Coupon> GetAllCoupon()
        {
            return couponObj.GetAllCoupons();
        }
    }
}