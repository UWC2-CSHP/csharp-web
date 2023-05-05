using HelloWorld.Models;

namespace HelloWorld
{
    // Normally, this interface would go into its own file.
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }

    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> Products
        {
            get
            {
                var items = new[]
                    {
                    new Product{ ProductId=101, Name = "Baseball", Description="balls", Price=14.20m},
                    new Product{ ProductId=102, Name="Football", Description="nfl", Price=9.24m},
                    new Product{ Name="Tennis ball", Price=4.24m},
                    new Product{ Name="Golf ball", Price=5.25m},
                };

                return items;
            }
        }
    }
}