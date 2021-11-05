using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore2.Models;

namespace BookStore2.Controllers
{
    public class CartController : ApiController
    {
        BookCart cartObj = new BookCart();
        public List<deleteObj> GetShowcart(int id) {
            return cartObj.getAllObj(id);
        }
        public int GetAllbookcartlist(string userName)
        {
            return cartObj.Getcart_Id(userName);
        }
        [HttpDelete,Route("api/fromcart")]
        public int Deletefromcart(Wishlist delObj) 
        {
            return cartObj.deletefromcart(delObj.bookTitle,delObj.userName);
        }
        [HttpPost, Route("api/addtoCart")]
        public int Postaddtocart(bookcartlist obj) 
        {
            return cartObj.addbooktocart(obj);
        }
        [HttpPost,Route("api/addQuantity")]
        public int Postaddbookquantity(bookincart obj)
        {
            return cartObj.addbookquantity(obj);
        }
        [HttpGet, Route("api/subQuantity")]
        public int GetsubQuantity(string bookName, int orderQty, string userName)
        {
            return cartObj.subbookquantity(bookName, orderQty, userName);
        }
    }
}
