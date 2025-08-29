using System.Collections.Generic;
using ProductInventoryManager.Models; // So we can use Product class

namespace ProductInventoryManager.Core
{
    public interface IInventoryManager
    {
        bool AddProduct(Product product);
        bool UpdateProduct(int productId, string newName, decimal? newPrice);
        bool RemoveProduct(int productId);
        bool TryGetProduct(int productId, out Product product);
        IEnumerable<Product> GetAllProducts();
        void InitializeSampleData();
    }
}
