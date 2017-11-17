using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using ClassLibrary1;
using ProductStart.Models;
using ProductStart.Providers;
using ProductStart.Repository;

namespace ProductStart.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductProvider _provider;

        public ProductsController(IProductProvider provider)
        {
            _provider = provider;
        }
        [Route("", Name = "AllProducts")]
        public IEnumerable<Entity> Get()
        {
            return _provider.GetAll();
        }

        // GET: Product
        [Route("{productid}", Name = "Product")]
        public IHttpActionResult Get(int productid)
        {
            var product = _provider.Get(productid);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
       
        [Route("", Name = "AddProduct")]
        public HttpResponseMessage PostProduct(Product product)
        {
            var newITem = _provider.Add(product);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, newITem);

            string uri = Url.Link("Product", new { productid = newITem.Id });

            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Route("{id}", Name = "UpdateProduct")]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            product.Id = id;
            var result = _provider.Update(id, product);
            if (result == null)
            {
                return NotFound();
            }

            return Content(HttpStatusCode.Accepted, result); //Request.CreateResponse<Product>(HttpStatusCode.Accepted, result);

        }

        [Route("{id}", Name = "DeleteProducts")]
        public IHttpActionResult DeleteProduct(int id)
        {
            var response = Get(id);

            if (response.GetType()==typeof(NotFoundResult))
            {
                return NotFound();
            }

            _provider.Delete(id);
            return Ok();
        }


    }
}