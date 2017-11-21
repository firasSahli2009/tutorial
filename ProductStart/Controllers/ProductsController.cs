using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using ClassLibrary1;
using ClassLibrary1.ModelFactory;
using Microsoft.Web.Http;
using ProductStart.Providers;

namespace ProductStart.Controllers
{
    //[Authorize]
    [ApiVersion("1", Deprecated = true)]
    [ApiVersion("2")]
    [RoutePrefix("api/products")]

    public class ProductsController : ApiController
    {
        private readonly IProductProvider _provider;
        private ProductModelFactory _modelFactory;

        public ProductsController(IProductProvider provider)
        {
            _provider = provider;
            
        }

        [Route("", Name = "Products")]
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

        [Route("", Name = "ProductsV2")]
        [HttpGet, MapToApiVersion("2.0")]
        public IHttpActionResult GetV2()
        {
            try
            {
                //return _provider.GetAll();
                _modelFactory = new ProductModelFactory(this.Request);

                List<ProductModel> productModels = new List<ProductModel>();
                var productLinks = _provider.GetAll();
                foreach (var product in productLinks)
                {
                    productModels.Add(_modelFactory.CreateProductModelV2(product));
                }

                return Ok(productModels);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }

        }

        // GET: Product
        [Route("~/api/product/{productid:int}", Name = "Product")]
        [HttpGet, MapToApiVersion("1.0")]
        public IHttpActionResult Get(int productid)
        {
            var product = _provider.Get(productid);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet, MapToApiVersion("2.0")]
        [Route("~/api/product/{productid:int}", Name = "ProductV2")]
        public IHttpActionResult GetV2(int productid)
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