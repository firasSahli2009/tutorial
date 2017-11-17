using System.Collections.Generic;

namespace ClassLibrary1
{
    public class Client: Entity, IClient
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public ClientCategory Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}