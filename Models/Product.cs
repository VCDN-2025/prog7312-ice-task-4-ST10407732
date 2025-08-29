using System;

namespace ProductInventoryManager.Models
{
    public class Product
    {
        public int ProductId { get; }
        public string Name { get; set; }
        public decimal Price { get; set; } // currency in R (you can format when displaying)

        public Product(int productId, string name, decimal price)
        {
            ProductId = productId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price;
        }

        public override string ToString()
        {
            // show price with currency symbol (R)
            return $"ID: {ProductId} | {Name} | Price: R{Price:N2}";
        }
    }
}
