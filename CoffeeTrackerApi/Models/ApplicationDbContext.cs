using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CoffeeTrackerApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Record> Records {  get; set; }

        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = CoffeeTrackerDB; TrustServerCertificate = True; Trusted_Connection = True;");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
