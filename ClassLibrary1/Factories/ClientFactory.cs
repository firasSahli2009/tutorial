using System.Collections.Generic;

namespace ClassLibrary1.Factories
{
    public static class ClientFactory
    {
        public static Client CreateClient(int id = 1, string name ="name", string address="address", ClientCategory category = ClientCategory.NoCategory)
        {
            return new Client { Id = id, Name = name, Address = address, Category = category, Products = new List<Product>()};
        }
    }
}