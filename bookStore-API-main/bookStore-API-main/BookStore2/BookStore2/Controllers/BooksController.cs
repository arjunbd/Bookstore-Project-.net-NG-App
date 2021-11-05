using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore2.Models;

namespace BookStore2.Controllers
{
    //[RoutePrefix("api/books")]
    
    public class BooksController : ApiController
    {
        Books bObj = new Books();
        [HttpGet]
        //[Route("")]
        // api/books
        public List<Books> GetAllBooks()
        {
            return bObj.GetAllBooks();
        }

        //api/books?b_name=Harry Potter
        public List<Books> GetBooksByName(string b_name)
        {
            return bObj.GetBooksByName(b_name);
        }

        //api/books?cat_name=Fiction
        public List<Books> GetBooksByCat(string cat_name)
        {
            return bObj.GetBooksByCat(cat_name);
        }

        //api/books?isbn=9780747532743
        public Books GetBooksByISBN(string isbn)
        {
            return bObj.GetBooksByISBN(isbn);
        }

        public Books GetBooksById(int bookId)
        {
            return bObj.GetBooksById(bookId);
        }
        //api/books?author_name=J K Rowling
        public List<Books> GetBooksByAuthor(string author_name)
        {
            return bObj.GetBooksByAuthor(author_name);
        }
        [HttpGet]
        [Route("api/activebooks")]
        //api/activebooks
        public List<Books> GetActiveBooks()
        {
            return bObj.GetActiveBooks();
        }
        [HttpGet]
        [Route("api/newbooks")]
        //api/newbooks
        public List<Books> GetNewBooks()
        {
            return bObj.GetNewBooks();
        }
        [HttpGet]
        [Route("api/featuredbooks")]
        //api/newbooks
        public Books GetFeaturedBook()
        {
            return bObj.GetFeaturedBook();
        }
        [HttpPost]
        public List<Books> Post(Books bookObj)
        {
            bObj.adminAddBooks(bookObj);
            return bObj.GetAllBooks();
        }
        [HttpPut] // use put in Postman and pass json object
        // api/books?bookId = 9
        public Books Put(int bookId, Books udtBookObj)
        {
            udtBookObj.adminUpdateBook(bookId, udtBookObj);
            return udtBookObj.GetBooksById(bookId);
        }
        [HttpDelete]
        //api/books?id=14
        public List<Books> Delete(int id)
        {
            bObj.adminDeleteBooks(id);
            return bObj.GetAllBooks();
        }
    }
}
