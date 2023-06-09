using System.Linq;
using Ziggle.Repository;

namespace Ziggle.Business
{
    public interface IProductManager
    {
        ProductModel[] ForCategory(int categoryId);
    }

    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductManager : IProductManager
    {
        private readonly IProductRepository productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ProductModel[] ForCategory(int categoryId)
        {
            return productRepository.ForCategory(categoryId).Select(t =>
                            new ProductModel
                            {
                                Id = t.Id,
                                Name = t.Name,
                                Price = t.Price,
                                Quantity = t.Quantity
                            })
                            .ToArray();
        }
    }
}