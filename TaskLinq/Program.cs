using Microsoft.EntityFrameworkCore;
using TaskLinq.Data;

namespace TaskLinq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDBContext db = new ApplicationDBContext();

            // 1- List all customers' first and last names along with their email addresses.
            // --------------------------------------------------------------------------------
            var result1 = db.Customers
                            .Select(c => new
                            {
                                c.FirstName,
                                c.LastName,
                                c.Email
                            }).ToList();


            // 2- Retrieve all orders processed by a specific staff member (e.g., staff_id = 3).
            // --------------------------------------------------------------------------------
            int VstaffId = 3;
            var result2 = db.Orders
                            .Where(o => o.StaffId == VstaffId)
                            .ToList();


            // 3- Get all products that belong to a category named "Mountain Bikes".
            // --------------------------------------------------------------------------------
            var result3 = db.Products
                            .Where(p => p.Category.CategoryName == "Mountain Bikes")
                            .ToList();

            var result31 = db.Products
                            .Include(p => p.Category)
                            .Where(p => p.Category.CategoryName == "Mountain Bikes")
                            .ToList();



            // 4- Count the total number of orders per store.
            // --------------------------------------------------------------------------------
            var result4 = db.Orders
                            .GroupBy(o => o.StoreId)
                            .Select(g => new
                            {
                                StoreId = g.Key,
                                OrderCount = g.Count()
                            }).ToList();


            // 5- List all orders that have not been shipped yet (shipped_date is null).
            // --------------------------------------------------------------------------------
            var result5 = db.Orders
                            .Where(e => e.ShippedDate == null);

            // 6- Display each customer’s full name and the number of orders they have placed.
            // --------------------------------------------------------------------------------
            var result6 = db.Customers
                            .Select(c => new
                            {
                                FullName = c.FirstName + " " + c.LastName,
                                OrdersCount = db.Orders.Count(o => o.CustomerId == c.CustomerId)
                            }).ToList();


            // 7- List all products that have never been ordered (not found in order_items).
            // --------------------------------------------------------------------------------
            var result7 = db.Products
                            .Where(p => !db.OrderItems.Any(oi => oi.ProductId == p.ProductId))
                            .ToList();


            // 8- Display products that have a quantity of less than 5 in any store stock.
            // --------------------------------------------------------------------------------
            var result8 = db.Stocks
                            .Where(s => s.Quantity < 5)
                            .Select(s => s.Product)
                            .Distinct()
                            .ToList();


            // 9- Retrieve the first product from the products table.
            // --------------------------------------------------------------------------------
            var result9 = db.Products.FirstOrDefault();

            // 10- Retrieve all products from the products table with a certain model year.
            // --------------------------------------------------------------------------------
            int VModelYear = 2022;
            var result10 = db.Products.Where(p => p.ModelYear == VModelYear);

            // 11- Display each product with the number of times it was ordered.
            // --------------------------------------------------------------------------------
            var result11 = db.OrderItems
                .GroupBy(o => o.ProductId)
                .Select(e => new
                {
                    product = e.Key,
                    count = e.Count()
                })
                .OrderBy(x => x.product)
                .ToList();



            // 12- Count the number of products in a specific category.
            // --------------------------------------------------------------------------------
            string VcategoryName = "Mountain Bikes";

            var result12 = db.Products
                .Join(db.Categories,
                      p => p.CategoryId,
                      c => c.CategoryId,
                      (p, c) => new { Product = p, Category = c })
                .Count(x => x.Category.CategoryName == VcategoryName);


            // 13- Calculate the average list price of products.
            // --------------------------------------------------------------------------------
            var result13 = db.Products
                             .Average(p => p.ListPrice);

            // 14- Retrieve a specific product from the products table by ID.
            // --------------------------------------------------------------------------------
            int VprodeuctID = 10;
            var result14 = db.Products.Where(e => e.ProductId == VprodeuctID);

            // 15- List all products that were ordered with a quantity greater than 3 in any order.
            // --------------------------------------------------------------------------------
            var result15 = db.Products
                             .Join(db.OrderItems,
                                    p => p.ProductId,
                                    oi => oi.ProductId,
                                    (p, oi) => new { Product = p, OrderItem = oi })
                             .Where(x => x.OrderItem.Quantity > 3)
                             .Select(x => x.Product)
                             .Distinct()
                             .ToList();

            // 16- Display each staff member’s name and how many orders they processed.
            // --------------------------------------------------------------------------------
            var result16 = db.Staffs
                             .Select(s => new
                             {
                                 FullName = s.FirstName + " " + s.LastName,
                                 OrdersCount = db.Orders.Count(o => o.StaffId == s.StaffId)
                             })
                              .ToList();

            // 17- List active staff members only (active = true) along with their phone numbers.
            // --------------------------------------------------------------------------------
            var result17 = db.Staffs
                                .Where(s => s.Active == 1)
                                .Select(s => new
                                {
                                    FullName = s.FirstName + " " + s.LastName,
                                    s.Phone
                                })
                                .ToList();

            // 18- List all products with their brand name and category name.
            // --------------------------------------------------------------------------------
            var result18 = db.Products
                             .Select(p => new
                             {
                                 p.ProductName,
                                 Brand = p.Brand.BrandName,
                                 Category = p.Category.CategoryName
                             })
                             .ToList();

            // 19- Retrieve orders that are completed.
            // --------------------------------------------------------------------------------
            var result19 = db.Orders
                            .Where(o => o.OrderStatus == 4)
                            .ToList();

            // 20- List each product with the total quantity sold (sum of quantity from order_items).
            // --------------------------------------------------------------------------------
            var result20 = db.Products
                                .Select(p => new
                                {
                                    p.ProductName,
                                    TotalSold = db.OrderItems
                                        .Where(oi => oi.ProductId == p.ProductId)
                                        .Sum(oi => (int?)oi.Quantity) ?? 0
                                })
                                .ToList();
        }
    }
}
