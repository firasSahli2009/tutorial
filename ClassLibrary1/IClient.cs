using System.Collections.Generic;

namespace ClassLibrary1
{
    public interface IClient
    {
        string Name { set; get; }
        string Address { set; get; }
        ClientCategory Category { set; get; }
        IEnumerable<Product> Products { set; get; }
    }
}