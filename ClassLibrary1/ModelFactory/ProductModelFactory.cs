using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.ModelFactory
{
    public class ProductModelFactory: ModelFactory
    {
        public ProductModelFactory(HttpRequestMessage request) : base(request)
        {
        }

        public ProductModel CreateProductModel(Product product)
        {
            return new ProductModel
            {
                Name = product.Name,
                CategoryName = Enum.GetName( typeof(ProductCategory), product.Category),
                SelfUrl = UrlHelper.Link("Product", new {productid = product.Id})
            };
        }

        public string CreateProductStringModel(Product product)
        {
            return UrlHelper.Link("Product", new {productid = product.Id});

        }
    }
}
