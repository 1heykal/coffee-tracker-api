using CoffeeTrackerApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeTrackerApi.Test
{
    public class InMemoryDbContext : ApplicationDbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        }

        public override void Dispose()
        {
            Database.EnsureDeleted();
        }
    }
}
