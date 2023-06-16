using System.Linq;
using Ziggle.Repository;

namespace Ziggle.Business
{
    public interface IShoppingCartManager
    {
        ShoppingCartModel Add(int userId, int productId, int quantity);
        bool Remove(int userId, int productId);
        ShoppingCartModel[] GetAll(int userId);
    }

    // Revise for Exercise 1: include name and price
    public class ShoppingCartModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }

    // Revise for Exercise 1:
    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;

        public ShoppingCartManager(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }

        public ShoppingCartModel Add(int userId, int productId, int quantity)
        {
            var item = shoppingCartRepository.Add(userId, productId, quantity);

            var product = productRepository.GetProduct(productId);

            // for exercise 1: return name and price
            return new ShoppingCartModel
            {
                ProductId = item.ProductId,
                ProductName = product.Name,
                ProductPrice = product.Price,
                Quantity = item.Quantity
            };
        }

        // Revise for Exercise 1: Get name and price
        public ShoppingCartModel[] GetAll(int userId)
        {
            var items = shoppingCartRepository.GetAll(userId)
                .Select(t => {
                    var product = productRepository.GetProduct(t.ProductId);

                    return new ShoppingCartModel
                    {
                        ProductId = t.ProductId,
                        ProductName = product.Name,
                        ProductPrice = product.Price,
                        Quantity = t.Quantity
                    };
                })
                .ToArray();

            return items;
        }


        public bool Remove(int userId, int productId)
        {
            return shoppingCartRepository.Remove(userId, productId);
        }
    }
}