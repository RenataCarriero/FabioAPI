using FabioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FabioAPI.Models
{

    public class Context : DbContext
    {
        public DbSet<OrderContract> Orders { get; set; }

        public DbSet<CustomerContract> Customers { get; set; }

        public Context(DbContextOptions<Context> options)
       : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = Test; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");



        }

    }
}