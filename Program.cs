using System;
using ProductInventoryManager.Core;
using ProductInventoryManager.Models;

namespace ProductInventoryManager
{
    class Program
    {
        static void Main(string[] args)
        {
            IInventoryManager inventory = new InventoryManager();
            inventory.InitializeSampleData();
            ShowMainMenu(inventory);
        }

        static void ShowMainMenu(IInventoryManager inventory)
        {
            while (true)
            {
                Console.WriteLine("\n--- Product Inventory Manager ---");
                Console.WriteLine("1. Display all products");
                Console.WriteLine("2. Search product by ID");
                Console.WriteLine("3. Add new product");
                Console.WriteLine("4. Update product");
                Console.WriteLine("5. Delete product");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option (1-6): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": DisplayAll(inventory); break;
                    case "2": SearchById(inventory); break;
                    case "3": AddProductFlow(inventory); break;
                    case "4": UpdateProductFlow(inventory); break;
                    case "5": DeleteProductFlow(inventory); break;
                    case "6": return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        static void DisplayAll(IInventoryManager inventory)
        {
            Console.WriteLine("\nInventory:");
            foreach (var p in inventory.GetAllProducts())
            {
                Console.WriteLine(p);
            }
        }

        static void SearchById(IInventoryManager inventory)
        {
            int id = ReadInt("Enter Product ID to search: ");
            if (inventory.TryGetProduct(id, out var product))
                Console.WriteLine(product);
            else
                Console.WriteLine($"Product with ID {id} not found.");
        }

        static void AddProductFlow(IInventoryManager inventory)
        {
            int id = ReadInt("Enter new Product ID: ");
            string name = ReadString("Enter product name: ");
            decimal price = ReadDecimal("Enter product price (numbers only): R");

            var product = new Product(id, name, price);
            if (inventory.AddProduct(product))
                Console.WriteLine("Product added successfully.");
            else
                Console.WriteLine("A product with that ID already exists.");
        }

        static void UpdateProductFlow(IInventoryManager inventory)
        {
            int id = ReadInt("Enter Product ID to update: ");
            if (!inventory.TryGetProduct(id, out var existing))
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.WriteLine("Leave field blank to keep current value.");
            Console.Write($"Current name: {existing.Name}. New name: ");
            var newName = Console.ReadLine();

            Console.Write($"Current price: R{existing.Price:N2}. New price (R): ");
            var newPriceRaw = Console.ReadLine();
            decimal? newPrice = null;
            if (!string.IsNullOrWhiteSpace(newPriceRaw) && decimal.TryParse(newPriceRaw, out decimal parsed))
                newPrice = parsed;

            if (inventory.UpdateProduct(id, newName, newPrice))
                Console.WriteLine("Product updated.");
            else
                Console.WriteLine("Update failed.");
        }

        static void DeleteProductFlow(IInventoryManager inventory)
        {
            int id = ReadInt("Enter Product ID to delete: ");
            if (inventory.RemoveProduct(id))
                Console.WriteLine("Product deleted.");
            else
                Console.WriteLine("Product not found.");
        }

        // helper input methods
        static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value)) return value;
                Console.WriteLine("Invalid integer. Try again.");
            }
        }

        static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal value)) return value;
                Console.WriteLine("Invalid decimal. Try again.");
            }
        }

        static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s)) return s;
                Console.WriteLine("Value cannot be empty.");
            }
        }
    }
}
