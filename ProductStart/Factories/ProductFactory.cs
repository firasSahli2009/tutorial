using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibrary1;

namespace ProductStart.Factories
{
    public class ProductFactory
    {
        public static Product CreateProduct(int id = 1, string name = "product name",  ProductCategory category= ProductCategory.Groceries, string description = "Product description")
        {
            return new Product {Id = id, Name = name, Category = category, Description = description};
        }
    }
}