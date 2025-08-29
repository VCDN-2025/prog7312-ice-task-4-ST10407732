using System;
using System.Collections.Generic;
using ProductInventoryManager.Models;

namespace ProductInventoryManager.Core
{
    public class InventoryManager : IInventoryManager
    {
        private readonly Dictionary<int, Product> _products = new();

        public void InitializeSampleData()
        {
            AddProduct(new Product(101, "Laptop", 5000m));
            AddProduct(new Product(102, "JBL Speaker", 8000m));
            AddProduct(new Product(103, "Tablet", 2300m));
            AddProduct(new Product(104, "Wireless Mouse", 250m));
            AddProduct(new Product(105, "Mechanical Keyboard", 1200m));
            AddProduct(new Product(106, "External HDD 1TB", 900m));
            AddProduct(new Product(107, "Smartphone", 7500m));
            AddProduct(new Product(108, "Webcam", 450m));
            AddProduct(new Product(109, "Monitor 24\"", 3200m));
            AddProduct(new Product(110, "USB-C Hub", 350m));
        }

        public bool AddProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (_products.ContainsKey(product.ProductId)) return false;
            _products[product.ProductId] = product;
            return true;
        }

        public bool UpdateProduct(int productId, string newName, decimal? newPrice)
        {
            if (!_products.TryGetValue(productId, out var existing)) return false;
            if (!string.IsNullOrWhiteSpace(newName)) existing.Name = newName;
            if (newPrice.HasValue) existing.Price = newPrice.Value;
            return true;
        }

        public bool RemoveProduct(int productId)
        {
            return _products.Remove(productId);
        }

        public bool TryGetProduct(int productId, out Product product)
        {
            return _products.TryGetValue(productId, out product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products.Values;
        }
    }
}
