namespace Ziggle.Repository
{
    public interface IShoppingCartRepository
    {
        ShoppingCartModel Add(int userId, int productId, int quantity);
        bool Remove(int userId, int productId);
        ShoppingCartModel[] GetAll(int userId);
    }

    public class ShoppingCartModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public ShoppingCartModel Add(int userId, int productId, int quantity)
        {
            var existingItem = DatabaseAccessor.Instance.ShoppingCartItems
                               .Where(t => t.UserId == userId
                                           && t.ProductId == productId)
                               .FirstOrDefault();

            if (existingItem != null)
            {
                // update the existing quantity from the shopping cart
                existingItem.Quantity += quantity;
                quantity = existingItem.Quantity;

                DatabaseAccessor.Instance.ShoppingCartItems.Update(existingItem);
            }
            else
            {
                DatabaseAccessor.Instance.ShoppingCartItems.Add(
                new Ziggle.ProductDatabase.ShoppingCartItem
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = quantity
                });
            }

            DatabaseAccessor.Instance.SaveChanges();

            return new ShoppingCartModel
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
            };
        }


        public ShoppingCartModel[] GetAll(int userId)
        {
            var items = DatabaseAccessor.Instance.ShoppingCartItems
                .Where(t => t.UserId == userId)
                .Select(t => new ShoppingCartModel
                {
                    UserId = t.UserId,
                    ProductId = t.ProductId,
                    Quantity = t.Quantity
                })
                .ToArray();
            return items;
        }

        public bool Remove(int userId, int productId)
        {
            var items = DatabaseAccessor.Instance.ShoppingCartItems
                                .Where(t => t.UserId == userId
                                            && t.ProductId == productId);

            if (items.Count() == 0)
            {
                return false;
            }

            DatabaseAccessor.Instance.ShoppingCartItems.Remove(items.First());

            DatabaseAccessor.Instance.SaveChanges();

            return true;
        }
    }
}