using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.ModelFactory
{
    public class ClientModelFactory : ModelFactory
    {
        private readonly ProductModelFactory _productModelFactory;

        public ClientModelFactory(HttpRequestMessage request) : base(request)
        {
            _productModelFactory = new ProductModelFactory(request);
        }

        public ClientModel CreateClientModel(Client client)
        {
            var productsLinks = new List<string>();


            if (client.Products != null && client.Products.Any())
            {
                foreach (var product in client.Products)
                {
                    productsLinks.Add(_productModelFactory.CreateProductStringModel(product)  );
                }
            }

            var result= new ClientModel
            {
                Name = client.Name,
                Address = client.Address,

                SelfUrl = UrlHelper.Link("Client", new { clientid = client.Id }),
                ProductsLinks = productsLinks
            };

            return result;
        }
    }
}
