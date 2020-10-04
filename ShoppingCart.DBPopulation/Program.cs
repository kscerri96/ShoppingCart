using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Data.Entities;
using System;
using System.Collections.Generic;

namespace ShoppingCart.DataSeeding
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppDBContext())
            {
                var products = new List<Products>
                {
                    //new Products
                    //{
                    //    Id = Guid.NewGuid().ToString(),
                    //    ProductName = "Apple iPad 10.2\" (7th Gen) 32GB Gold Wifi Tablet",
                    //    ProductDescription = "The new ipad combines the power of capability of a computer with the ease of use.",C:\Users\kscerri\source\repos\ShoppingCart\ShoppingCart.DBPopulation\Program.cs
                    //    ProductPrice = 500,
                    //    Quantity = 1000,
                    //    ProductImageName = "Apple Ipad",
                    //    ProductImagePath = "/Images/Apple Ipad.jpg"
                    //}
                    //},
                    //new Products
                    //{
                    //    Id =  Guid.NewGuid().ToString(),
                    //    ProductName = "Lenovo Tab M7 7\" 16GB 1GB Android WiFi Gray Tablet",
                    //    ProductDescription = "The Tab M7 (2nd Gen) looks great and packs plenty of entertaining features. And with the latest Android Pie OS, this tablet gives you access to all of your favorite apps available on the Google Play store.",
                    //    ProductPrice = 85,
                    //    Quantity = 500,
                    //    ProductImageName = "Lenovo Tab",
                    //    ProductImagePath = "Lenovo Tab.jpg"
                    //},

                    //new Products
                    //{
                    //    Id =  Guid.NewGuid().ToString(),
                    //    ProductName = "Samsung Galaxy Tab S6 Lite 10.4\" 64GB 4GB Android Wifi Blue Tablet",
                    //    ProductDescription = "Galaxy Tab S6 Lite seamlessly syncs your Galaxy smartphone, so you never miss a call when it comes in",
                    //    ProductPrice = 399,
                    //    Quantity = 5000,
                    //    ProductImageName = "Galaxy Tab",
                    //    ProductImagePath = "/Images/Galaxy Tab.jpg"
                    //},
                    //new Products
                    //{
                    //    Id =  Guid.NewGuid().ToString(),
                    //    ProductName = "Microsoft Surface Go 2 10.5\" Intel Pentium 64GB SSD 4GB RAM Win10 Pro Tablet PC",
                    //    ProductDescription = "Keep up with everyone from just about anywhere",
                    //    ProductPrice = 600,
                    //    Quantity = 5000,
                    //    ProductImageName = "Microsoft Surface",
                    //    ProductImagePath = "/Images/Microsoft Surface.jpg"
                    //},

                    //new Products
                    //{
                    //    Id =  Guid.NewGuid().ToString(),
                    //    ProductName = "Samsung Galaxy Tab A (2019) 10.1\" T510 32GB Android Wifi Black Tablet",
                    //    ProductDescription = "With a sleek aluminum body, the Galaxy Tab A is sytlish yet durable.",
                    //    ProductPrice = 200,
                    //    Quantity = 1000,
                    //    ProductImageName = "Galaxy TabA",
                    //    ProductImagePath = "/Images/Galaxy TabA.jpg"
                    //},
                    //new Products
                    //{
                    //    Id =  Guid.NewGuid().ToString(),
                    //    ProductName = "Apple iPad Mini (5th Gen) 64GB Silver Wifi Tablet",
                    //    ProductDescription = "iPad mini features a thin, light and portable design that makes it the perfect on-the-go companion.",
                    //    ProductPrice = 300,
                    //    Quantity = 3000,
                    //    ProductImageName = "Ipad Mini",
                    //    ProductImagePath = "/Images/Ipad Mini.jpg"
                    //}
                };
                db.Products.AddRange(products);
                db.SaveChanges();

                //Console.WriteLine("Querying for a blog");
                //Guid g = new Guid("5753DD17-1927-4584-9A87-66BCCB4F1EC9");
                //var blog = db.Products
                //    .Where(b => b.Id == g)
                //    .OrderBy(b => b.Id)
                //    .First();


                //Console.WriteLine(blog.ProductDescription + blog.ProductName);
                //db.Remove(blog);
                //db.SaveChanges();

            }
        }
    }
}
