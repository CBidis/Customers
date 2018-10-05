using Customers.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Customers.DAL
{
    /// <summary>
    /// Data Access Layer Class
    /// </summary>
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options)
        : base(options)
        {           
        }

        /// <summary>
        /// Configures One to One Mapping Between Customer and CustomerContact
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(a => a.CustomerContact)
                .WithOne(b => b.Customer)
                .HasForeignKey<CustomerContact>(b => b.CustomerId);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
    }
}
