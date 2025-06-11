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
            int staffId = 3;
            var result2 = db.Orders
                .Where(o => o.StaffId == staffId)
                .ToList();


            // 3- Get all products that belong to a category named "Mountain Bikes".
            // --------------------------------------------------------------------------------
            var result3 = db.Products
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


            // 8- Display products that have a quantity of less than 5 in any store stock.
            // --------------------------------------------------------------------------------


            // 9- Retrieve the first product from the products table.
            // --------------------------------------------------------------------------------
            var result9 = db.Products.FirstOrDefault();

            // 10- Retrieve all products from the products table with a certain model year.
            // --------------------------------------------------------------------------------
            int VModelYear = 2022;
            var result10 = db.Products.Where(p => p.ModelYear == VModelYear);

            // 11- Display each product with the number of times it was ordered.
            // --------------------------------------------------------------------------------


            // 12- Count the number of products in a specific category.
            // --------------------------------------------------------------------------------


            // 13- Calculate the average list price of products.
            // --------------------------------------------------------------------------------


            // 14- Retrieve a specific product from the products table by ID.
            // --------------------------------------------------------------------------------


            // 15- List all products that were ordered with a quantity greater than 3 in any order.
            // --------------------------------------------------------------------------------


            // 16- Display each staff member’s name and how many orders they processed.
            // --------------------------------------------------------------------------------


            // 17- List active staff members only (active = true) along with their phone numbers.
            // --------------------------------------------------------------------------------


            // 18- List all products with their brand name and category name.
            // --------------------------------------------------------------------------------


            // 19- Retrieve orders that are completed.
            // --------------------------------------------------------------------------------


            // 20- List each product with the total quantity sold (sum of quantity from order_items).
            // --------------------------------------------------------------------------------
        }
    }
}
