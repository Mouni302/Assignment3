using System;
using System.Collections.Generic;

namespace Assignment3
{

    class Product
    {
        
        public static string Brand = "H&M";
        public string Pcode { get; private set; } 
        public string Pname { get; set; }
        public int QtyInStock { get; set; }
        public float DiscountAllowed { get; set; }
        public float Price { get; set; } = 100; 

        
        public Product(string pcode, string pname, int qtyInStock, float discountAllowed)
        {
            Pcode = pcode;
            Pname = pname;
            QtyInStock = qtyInStock;
            DiscountAllowed = discountAllowed;
        }

        // Display product details
        public void DisplayDetails()
        {
            Console.WriteLine($"Product Code: {Pcode}");
            Console.WriteLine($"Product Name: {Pname}");
            Console.WriteLine($"Quantity in Stock: {QtyInStock}");
            Console.WriteLine($"Discount Allowed: {DiscountAllowed}%");
            Console.WriteLine($"Price: ${Price}");
            Console.WriteLine($"Brand: {Brand}");
        }

        // Calculate total amount with discount
        public float CalculateTotal(int qty)
        {
            
            float discount = 50;
            return Price * qty * (1 - discount / 100);
        }
    }

    class Store
    {
        private List<Product> products = new List<Product>();

       
        public void AddProduct()
        {
            Console.Write("Enter product code (unique): ");
            string pcode = Console.ReadLine();
            Console.Write("Enter product name: ");
            string pname = Console.ReadLine();
            Console.Write("Enter quantity in stock: ");
            int qtyInStock = int.Parse(Console.ReadLine());
            Console.Write("Enter discount allowed percentage: ");
            float discountAllowed = float.Parse(Console.ReadLine());

            Product product = new Product(pcode, pname, qtyInStock, discountAllowed);
            products.Add(product);
            Console.WriteLine($"Product {pname} added successfully!");
        }

        // Display all products
        public void DisplayProducts()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No products available.");
                return;
            }

            Console.WriteLine("Product List:");
            foreach (var product in products)
            {
                product.DisplayDetails();
                Console.WriteLine("--------------------------");
            }
        }

        // Customer orders a product
        public void OrderProduct()
        {
            Console.Write("Enter the product name you want to purchase: ");
            string pname = Console.ReadLine();

            Product found = null;
            foreach (var product in products)
            {
                if (product.Pname.Equals(pname, StringComparison.OrdinalIgnoreCase))
                {
                    found = product;
                    break;
                }
            }

            if (found == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write($"Enter quantity of {pname} you want to purchase: ");
            int qty = int.Parse(Console.ReadLine());

            if (qty > found.QtyInStock)
            {
                Console.WriteLine("Not enough stock available.");
                return;
            }

            float totalAmount = found.CalculateTotal(qty);
            Console.WriteLine($"Total amount for {qty} {pname}(s): ${totalAmount:F2}");
            GenerateBill(found, qty, totalAmount);
        }

       
        private void GenerateBill(Product product, int qty, float totalAmount)
        {
            Console.WriteLine("\n-------- Bill --------");
            Console.WriteLine($"Product: {product.Pname}");
            Console.WriteLine($"Quantity: {qty}");
            Console.WriteLine($"Total Amount (after discount): ${totalAmount:F2}");
            Console.WriteLine("----------------------");
            Console.WriteLine("Thank you for your purchase!");
        }

        
        public void AdminActions()
        {
            while (true)
            {
                Console.WriteLine("\nAdmin Menu:");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Display Products");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    AddProduct();
                }
                else if (choice == "2")
                {
                    DisplayProducts();
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        
        public void CustomerActions()
        {
            while (true)
            {
                Console.WriteLine("\nCustomer Menu:");
                Console.WriteLine("1. Order Product");
                Console.WriteLine("2. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    OrderProduct();
                }
                else if (choice == "2")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Store store = new Store();

            while (true)
            {
                Console.Write("\nWho are you? (Admin/Customer): ");
                string userType = Console.ReadLine().Trim().ToLower();

                if (userType == "admin")
                {
                    store.AdminActions();
                }
                else if (userType == "customer")
                {
                    store.CustomerActions();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please type 'Admin' or 'Customer'.");
                }
            }
        }
    }

}

