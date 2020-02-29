using System;

namespace StockMarketApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public string EmailSeller { get; set; }

        public string EmailCustomer { get; set; }

        public DateTime DateTimeSeller { get; set; }

        public DateTime DateTimeCustomer { get; set; }

        public DateTime DateTimeCompleted { get; set; }
    }
}
