using HelloWorld.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace HelloWorld
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }

    public class ProductRepository : IProductRepository
    {
        private IMemoryCache memoryCache;

        public ProductRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IEnumerable<Product> Products
        {
            get
            {
                Product[] items;

                // See if "MyProducts" is cached, if it is, then it is stored in items
                if (!memoryCache.TryGetValue("MyProducts", out items))
                {
                    // Retrieve items from the database
                    items = new[]
                        {
                        new Product{ ProductId=101, Name = "Baseball", Description="balls", Price=14.20m},
                        new Product{ ProductId=102, Name="Football", Description="nfl", Price=9.24m},
                        new Product{ Name="Tennis ball", Price=4.24m} ,
                        new Product{ Name="Golf ball", Price=5.25m},
                    };

                    // Store the "database" results in the cache for 20 seconds
                    //memoryCache.Set("MyProducts", items,
                    //    new MemoryCacheEntryOptions()
                    //    .SetAbsoluteExpiration(System.TimeSpan.FromSeconds(20)));

                    // Exercise: Sliding Cache
                    memoryCache.Set("MyProducts", items,
                        new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(System.TimeSpan.FromSeconds(5)));
                }

                // Always retrieve the "database" results from the cache
                return (Product[])memoryCache.Get("MyProducts"); ;
            }
        }
    }
}