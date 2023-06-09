using Ziggle.ProductDatabase;

namespace Ziggle.Repository
{
    // This class is ONLY usable within the Repository project
    // This is because we don't want another other projects to 
    // directly access the database. That defeats the purpose
    // of the layers.
    internal class DatabaseAccessor
    {
        static DatabaseAccessor()
        {
            Instance = new ProductDbContext();
        }

        public static ProductDbContext Instance { get; private set; }
    }
}