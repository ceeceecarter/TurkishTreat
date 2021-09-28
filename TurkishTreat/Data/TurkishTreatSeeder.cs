using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using TurkishTreat.Data.Entities;

namespace TurkishTreat.Data
{
    public class TurkishTreatSeeder
    {
        private readonly TurkishTreatDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<StoreUser> _userManager;

        public TurkishTreatSeeder(TurkishTreatDbContext context, IWebHostEnvironment environment,
            UserManager<StoreUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            StoreUser user = await _userManager.FindByEmailAsync("cecilia@turkishtreat.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Cecilia",
                    LastName = "Carter",
                    Email="cecilia@turkishtreat.com",
                    UserName = "cecilia@turkishtreat.com"
                };
                var result = await _userManager.CreateAsync(user, "Passw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            if (!_context.Products.Any())
            {
                var filePath = Path.Combine(_environment.ContentRootPath, "Data/art.json");
                var json = await File.ReadAllTextAsync(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                // ReSharper disable once PossibleMultipleEnumeration
                if (products != null) _context.Products.AddRange(products);

                var order = _context.Orders.FirstOrDefault(i => i.Id == 1);

                if (order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            // ReSharper disable once PossibleMultipleEnumeration
                            Product = products.First(),
                            Quantity = 5,
                            // ReSharper disable once PossibleMultipleEnumeration
                            UnitPrice = products.First().Price
                        }
                    };
                    await _context.SaveChangesAsync();
                }

                //var order = new Order()
                //{
                //    OrderDate = DateTime.Today,
                //    OrderNumber = "100000",
                //    Items = new List<OrderItem>()
                //    {
                //        new OrderItem()
                //        {
                //            // ReSharper disable once PossibleMultipleEnumeration
                //            // ReSharper disable once AssignNullToNotNullAttribute
                //            Product = products.First(),
                //            Quantity = 5,

                //            // ReSharper disable once PossibleMultipleEnumeration
                //            UnitPrice = products.First().Price
                //        }
                //    }
                //};
                //_context.Orders.Add(order);
                //_context.SaveChanges();
            }
        }
    }
}
