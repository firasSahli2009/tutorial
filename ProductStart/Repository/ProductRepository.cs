using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibrary1;
using ProductStart.Controllers;
using ProductStart.Models;


namespace ProductStart.Repository
{
    public class ProductRepository : IProductRepository
    {
        public readonly ProductTableSimulator Products= ProductTableSimulator.Instance;
        

        public ProductRepository()
        {
            
        }


        public IEnumerable<Product> GetAll()
        {
            return Products.ListAll();
        }

        public Product Get(int id)
        {
            return Products.FindById(id);
        }

        public Product Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentException("product cannot be null");
            }
            Products.Add(product);
            return product;
        }

        public Product Update(int id, Product product)
        {
            product.Id = id;
            return Products.Update(product);
        }

        public void Delete(int id)
        {
            Products.Delelete(Products.FindById(id));
        }
    }
}