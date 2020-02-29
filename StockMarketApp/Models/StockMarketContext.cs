using System.Data.Entity;

namespace StockMarketApp.Models
{
    public class StockMarketContext: DbContext
    {
        public StockMarketContext() : base("StockMarketConnection") { }

        public DbSet<CustomerOrder> CustomerOrders { get; set; }

        public DbSet<SellerOrder> SellerOrders { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
