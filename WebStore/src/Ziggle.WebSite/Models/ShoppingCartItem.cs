namespace Ziggle.WebSite.Models
{
    // For exercise 1: we add name and price to our model
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
