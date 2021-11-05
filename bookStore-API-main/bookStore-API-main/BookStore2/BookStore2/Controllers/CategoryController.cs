using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore2.Models;

namespace BookStore2.Controllers
{
    public class CategoryController : ApiController
    {
        Category catObj = new Category();
        [HttpGet]
        // api/category
        public List<Category> GetAllCategories()
        {
            return catObj.GetAllCategory();
        }
        [HttpGet]
        // api/category?id=1
        public Category GetCategoryById(int id)        {            return catObj.GetCategoryById(id);        }
        [HttpPost] //use post in Postman and pass json object
        // api/category
        public List<Category> Post(Category categoryObj)
        {
            catObj.adminAddCategory(categoryObj);
            return catObj.GetAllCategory();
        }
        [HttpPut] // use put in Postman and pass json object
        // api/category?catId = 9
        public Category Put(int catId, Category udtCategoryObj)
        {
            catObj.adminUpdateCategory(catId,udtCategoryObj);
            return catObj.GetCategoryById(catId);
        }
        [HttpDelete]
        // api/category?catId=9
        public List<Category> Delete(int catId)
        {
            catObj.adminDeleteCategory(catId);
            return catObj.GetAllCategory();
        }


    }
}
