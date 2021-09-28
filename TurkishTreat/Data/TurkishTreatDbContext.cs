using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using TurkishTreat.Data.Entities;

namespace TurkishTreat.Data
{
    public class TurkishTreatDbContext : IdentityDbContext<StoreUser>
    {

        public TurkishTreatDbContext(DbContextOptions<TurkishTreatDbContext> options):base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
