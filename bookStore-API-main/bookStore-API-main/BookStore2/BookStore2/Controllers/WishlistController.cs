using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore2.Models;
namespace BookStore2.Controllers
{
    public class WishlistController : ApiController
    {
        Wishlist wishlistObj = new Wishlist();

        //api/Wishlist?userName=John
        public List<Books> GetWishlist(string userName)
        {
            return wishlistObj.GetWishlist(userName);
        }

        [HttpPost]
        //api/Wishlist
        public int PostAddToWishlist(Wishlist newWishlistObj)
        {
            return wishlistObj.AddToWishlist(newWishlistObj);
        }

        [HttpDelete]
        //api/Wishlist
        public int DeleteFromWishlist(Wishlist newWishlistObj)
        {
            return wishlistObj.DeleteFromWishlist(newWishlistObj);
        }
    }
}
