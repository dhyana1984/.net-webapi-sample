using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPISample.Filter;
using WebAPISample.IRepository;
using WebAPISample.Models;
using WebAPISample.Repository;

namespace WebAPISample.Controllers
{

    public class ProductsController : ApiController
    {
        //Product[] products = new Product[]
        //{
        //    new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
        //    new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
        //    new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M
        //    }
        //};

        //[Route("Html/api/Products")]
        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return products;
        //}
        //[HttpGet]
        //[Route("Html/api/Products/{id}")]
        //public IHttpActionResult GetProduct(int id)
        //{
        //    var product = products.FirstOrDefault((p) => p.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}


        static readonly IProductRepository repository = new ProductRepository();
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        public Product GetProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                  p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        [ModelValidationFilter]
        public HttpResponseMessage PostProduct(Product item)
        {
            //if (ModelState.IsValid) //模型验证
            //{
                item = repository.Add(item);
                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item); //响应Http201，Created.CreateResponse 方法将会创建 HttpResponseMessage，并自动将 Product 对象的序列化表示形式写入到响应消息的正文中。
                string uri = Url.Link("DefaultApi", new { id = item.Id });
                response.Headers.Location = new Uri(uri);                                     //在响应标头中包含新增的产品uri
                return response;                                                              //此方法返回类型现在是HttpResponseMessage。
                                                                                              //通过返回HttpResponseMessage而不是产品，
                                                                                              //我们可以控制的 HTTP 响应消息，包括状态代码和位置标头的详细信息。
            //}
            //else
            //{
            //    return new HttpResponseMessage(HttpStatusCode.BadRequest);                  //模型验证不通过返回badrequest
            //}
        }

        public void PutProduct(int id, Product product)
        {
            product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}
