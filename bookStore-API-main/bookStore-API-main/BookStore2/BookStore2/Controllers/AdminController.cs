using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore2.Models;

namespace BookStore2.Controllers
{
    public class AdminController : ApiController
    {
        admin adminObj = new admin();
        Books bookObj = new Books();
        Users userObj = new Users();
            // Checks the admin username and password and returns a bool
        // api/Admin
        public bool PostCheckAdminPassword(loginCredentials objadm)
        {
            return adminObj.checkAdminPassword(objadm.userName, objadm.userPassword);
        }
        // Allows an admin to activate a user
        //api/Admin?ActivateUserName=John
        public List<Users> GetAdminActivateUser(string activateUserName)
        {
            adminObj.AdminActivateUser(activateUserName);
            return userObj.getallUsers();
        }

        // Allows an admin to deactivate a user
        //api/Admin?deactivateUserName=John
        public List<Users> GetAdminDeactivateUser(string deactivateUserName)
        {
            adminObj.AdminDeactivateUser(deactivateUserName);
            return userObj.getallUsers();
        }

        // Allows an admin to add a coupon
        //api/Admin?couponName=30% Discount&discountRate=.3
        public int GetAdminAddCoupon(string couponName, double discountRate)
        {
            return adminObj.AdminAddCoupon(couponName, discountRate);
        }
        [HttpPost,Route("api/addBook")]
        public int PostAddBook(Books bookObj) {
            return bookObj.adminAddBooks(bookObj);
        }
    }
}
