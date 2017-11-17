using System.Collections.Generic;
using ClassLibrary1;
using ProductStart.Models;

namespace ProductStart.Providers
{
    public interface IProductProvider
    {
        IEnumerable<Product> GetAll();
        Product Get(int id);
        Product Add(Product product);
        Product Update(int id, Product product);
        void Delete(int id);
    }
}