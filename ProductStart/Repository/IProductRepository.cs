using System.Collections.Generic;
using ClassLibrary1;
using ProductStart.Models;

namespace ProductStart.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Entity> GetAll();
        Product Get(int id);
        Product Add(Product product);
        Product Update(int id, Product product);
        void Delete(int id);
    }
}