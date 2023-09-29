using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Infrastructure
{
    public class EntityFrameworkCoreDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public EntityFrameworkCoreDbContext(DbContextOptions<EntityFrameworkCoreDbContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(GetCustomerArray());
        }

        #region db inizialize
        private Customer[] GetCustomerArray()
        {
            var customers = new Customer[]
            {
                new Customer(){Firstname="Петр",Lastname="Петров", Id=1},
                new Customer(){Firstname="Иван",Lastname="Иванов",Id=2},
                new Customer(){Firstname="Василий",Lastname="Васильев",Id=3}
            };
            return customers;
        }
        #endregion

    }
}
