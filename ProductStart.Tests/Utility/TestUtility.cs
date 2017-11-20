using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using ClassLibrary1.ModelFactory;

namespace ProductStart.Tests.Utility
{
    public class TestUtility
    {
        public static List<Product> CreateProductList()
        {
            return new List<Product>
                {
                    new Product {Category = ProductCategory.Groceries, Id = 1, Name = "prod1", Description = "Desc1"},
                    new Product {Category = ProductCategory.Hardware, Id = 2, Name = "prod2", Description = "Desc2"},
                    new Product {Category = ProductCategory.Toys, Id = 3, Name = "prod3", Description = "Desc3"},
                };
        }

        public static List<ProductModel> CreateProductModelList()
        {
            var modelFactiry = new ProductModelFactory(null);
            var result = new List<ProductModel>();

            result.Add(
                modelFactiry.CreateProductModel(new Product
                {
                    Category = ProductCategory.Groceries,
                    Id = 1,
                    Name = "prod1",
                    Description = "Desc1"
                }));

            result.Add(
                modelFactiry.CreateProductModel(new Product
                {
                    Category = ProductCategory.Hardware,
                    Id = 2,
                    Name = "prod2",
                    Description = "Desc2"
                }));

            result.Add(
                modelFactiry.CreateProductModel(new Product
                {
                    Category = ProductCategory.Toys,
                    Id = 3,
                    Name = "prod3",
                    Description = "Desc3"
                }));

            return result;
        }
    }
}
