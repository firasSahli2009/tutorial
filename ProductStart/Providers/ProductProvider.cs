using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibrary1;
using ProductStart.Models;
using ProductStart.Repository;

namespace ProductStart.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly IProductRepository _productRepository;


        public ProductProvider(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Entity> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product Get(int id)
        {
            return _productRepository.Get(id);
        }

        public Product Add(Product product)
        {
            return _productRepository.Add(product);
        }

        public Product Update(int id, Product product)
        {
            return _productRepository.Update(id, product);
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }
    }
}