using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using TurkishTreat.Data.Entities;

namespace TurkishTreat.Data
{
    public class TurkishTreatSeeder
    {
        private readonly TurkishTreatDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TurkishTreatSeeder(TurkishTreatDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();
            if (!_context.Products.Any())
            {
                var filePath = Path.Combine(_environment.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                // ReSharper disable once PossibleMultipleEnumeration
                if (products != null) _context.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "100000",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            // ReSharper disable once PossibleMultipleEnumeration
                            // ReSharper disable once AssignNullToNotNullAttribute
                            Product = products.First(),
                            Quantity = 5,

                            // ReSharper disable once PossibleMultipleEnumeration
                            UnitPrice = products.First().Price
                        }
                    }
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
        }
    }
}
