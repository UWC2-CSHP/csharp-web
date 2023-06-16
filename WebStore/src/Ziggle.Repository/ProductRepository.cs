using System.Linq;

namespace Ziggle.Repository
{
    public interface IProductRepository
    {
        ProductModel[] ForCategory(int categoryId);
        
        // add this to our interface for exercise 1
        ProductModel GetProduct(int productId); 
    }

    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }



    public class ProductRepository : IProductRepository
    {
        public ProductModel[] ForCategory(int categoryId)
        {
            // Because a product can be in multiple categories,
            // we use t.Categories.Any( )
            var products = DatabaseAccessor.Instance
                                           .Products
                                           .Where(t => t.Categories.Any(r => r.CategoryId == categoryId));

            return products
                    .Select(t => new ProductModel
                    {
                        Id = t.ProductId,
                        Name = t.ProductName,
                        Price = t.ProductPrice,
                        Quantity = t.ProductQuantity
                    })
                    .ToArray();
        }

        // Implement GetProduct 
        public ProductModel GetProduct(int productId)
        {
            var product = DatabaseAccessor.Instance.Products.First(t => t.ProductId == productId);

            return new ProductModel
            {
                Id = product.ProductId,
                Name = product.ProductName,
                Price = product.ProductPrice,
                Quantity = product.ProductQuantity,
            };
        }

    }
}