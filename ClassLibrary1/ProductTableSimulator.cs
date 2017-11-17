
using System.Collections.Generic;


namespace ClassLibrary1
{
    public class ProductTableSimulator
    {
        private static ProductTableSimulator _instance;
        private static ProductDataSet _productTable;
        private int _nextId = 1;

        private static readonly object Padlock = new object();

        public static ProductTableSimulator Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new ProductTableSimulator());
                }
            }
        }

        private ProductTableSimulator()
        {
            _productTable = new ProductDataSet();

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
                Name = "Hardware Hard"
            });

        }

        public Product Add(Product product)
        {
            product.Id=_nextId++;
            return _productTable.Add(product);
            
        }

        public Product Update(Product product)
        {
            return _productTable.Update(product);
        }

        public void Delelete(Product product)
        {
            _productTable.Delete( product.Id);
        }

        public Product FindById(int id)
        {
            return _productTable.FindById(id);
        }

        public List<Product> ListAll()
        {
            return _productTable.Elements;
        }
    }
}