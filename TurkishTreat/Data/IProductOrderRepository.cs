using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TurkishTreat.Data.Entities;

namespace TurkishTreat.Data
{
    public interface IProductOrderRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);
        void AddEntity(object model);
        bool SaveAll();
    }
}
