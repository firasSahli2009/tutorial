namespace ClassLibrary1.Factories
{
    public class ProductFactory
    {
        public static Product CreateProduct(int id = 1, string name = "product name",  ProductCategory category= ProductCategory.Groceries, string description = "Product description")
        {
            return new Product {Id = id, Name = name, Category = category, Description = description};
        }
    }
}