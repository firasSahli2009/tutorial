
using System.Collections.Generic;


namespace ClassLibrary1
{
    public class ProductTableSimulator
    {
        private static ProductTableSimulator _instance;
        private static ProductDataSet ProductTable;
        private int _nextId = 1;

        private static readonly object Padlock = new object();

        public static ProductTableSimulator Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new ProductTableSimulator();
                    }
                    return _instance;
                }
            }
        }

        private ProductTableSimulator()
        {
            ProductTable = new ProductDataSet();

            Add(new Product
            {
                Category = ProductCategory.Groceries,
                Description = "Product 1 description",
                Name = "Product 1"
            });
            Add(new Product
            {
                Category = ProductCategory.Toys,
                Description = "Toy 1 description",
                Name = "Toys 1"
            });

            Add(new Product
            {
                Category = ProductCategory.Hardware,
                Description = " Hardware 1 description",
                Name = "Hardware 1"
            });

        }

        public Product Add(Product product)
        {
            product.Id=_nextId++;
            return ProductTable.Add(product);
            
        }

        public Product Update(Product product)
        {
            return ProductTable.Update(product);
        }

        public void Delelete(Product product)
        {
            ProductTable.Delete( product.Id);
        }

        public Product FindById(int id)
        {
            return ProductTable.FindById(id);
        }

        public List<Entity> ListAll()
        {
            return ProductTable.Elements;
        }
    }
}