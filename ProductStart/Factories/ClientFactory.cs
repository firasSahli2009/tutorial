using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibrary1;

namespace ProductStart.Factories
{
    public static class ClientFactory
    {
        public static Client CreateClient(int id = 1, string name ="name", string address="address", ClientCategory category = ClientCategory.NoCategory)
        {
            return new Client { Id = id, Name = name, Address = address, Category = category, Products = new List<Product>()};
        }
    }
}