using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TurkishTreat.Data.Entities;

namespace TurkishTreat.Data
{
    public class ProductOrderRepository : IProductOrderRepository
    {
        private readonly TurkishTreatDbContext _context;
        private readonly ILogger<ProductOrderRepository> _logger;

        public ProductOrderRepository(TurkishTreatDbContext context, ILogger<ProductOrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            _logger.LogInformation("GetAllProducts is called");
            return _context.Products.OrderBy(i => i.Title).ToList();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products
                .Where(i => i.Category == category)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefault(i => i.Id == id);
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .ToList();
            }

            return _context.Orders
                .ToList();
        }
    }
}
