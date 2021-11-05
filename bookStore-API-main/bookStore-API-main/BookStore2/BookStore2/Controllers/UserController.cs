using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore2.Models;

namespace BookStore2.Controllers
{
    public class UserController : ApiController
    {

        Users UserObj = new Users();
        public Users GetUserDetails(string userName)
        {
            return UserObj.getUserDetails(userName);
        }
        [HttpPost,Route("api/register")]
        public int PostuserRegister(Users Obj) 
        {
            return UserObj.userRegister(Obj);
        }
        [HttpPost,Route("api/Login")]
        public string PostonLogin(loginCredentials obj) 
        {
            return UserObj.onLogin(obj);
        }
        public int GetchangeAdress(string userName, string newShippingAddress)
        {
            return UserObj.changeShipping(userName, newShippingAddress); 
        }
        [HttpGet,Route("api/AllUsers")]
        public List<Users> Getuser() {
            return UserObj.getallUsers();
        }
      
    }
}
