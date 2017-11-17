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
using ClassLibrary1.ModelFactory;


namespace ProductStart.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductProvider _provider;
        private ProductModelFactory _modelFactory;

        public ProductsController(IProductProvider provider)
        {
            _provider = provider;
            
        }

        [Route("", Name = "AllProducts")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                //return _provider.GetAll();

                _modelFactory = new ProductModelFactory(this.Request);

                List<ProductModel> productModels = new List<ProductModel>();
                var productLinks = _provider.GetAll();
                foreach (var product in productLinks)
                {
                    productModels.Add(_modelFactory.CreateProductModel(product));
                }

                return Ok(productModels);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
            
        }

        // GET: Product
        [Route("{productid}", Name = "Product")]
        [HttpGet]
        public IHttpActionResult Get(int productid)
        {
            var product = _provider.Get(productid);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
       
        [HttpPost]
        [Route("", Name = "AddProduct")]
        public HttpResponseMessage PostProduct(Product product)
        {
            var newITem = _provider.Add(product);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, newITem);

            string uri = Url.Link("Product", new { productid = newITem.Id });

            response.Headers.Location = new Uri(uri);
            return response;
        }

        [HttpPut]
        [HttpPatch]
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

        [HttpDelete]
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