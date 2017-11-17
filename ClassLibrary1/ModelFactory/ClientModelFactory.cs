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
        public ClientModelFactory(HttpRequestMessage request) : base(request)
        {
        }

        public ClientModel CreateClientModel(Client client)
        {
            return new ClientModel
            {
                Name = client.Name,
                Address = client.Address,
                Category = client.Category,

                SelfUrl = UrlHelper.Link("Client", new { clientid = client.Id })
            };
        }
    }
}
