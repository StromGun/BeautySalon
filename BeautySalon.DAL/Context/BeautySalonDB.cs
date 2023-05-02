using BeautySalon.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.DAL.Context
{
    public class BeautySalonDB : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<OrderService> OrderServices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
                .HasMany(e => e.Orders)
                .WithMany(e => e.Services)
                .UsingEntity<OrderService>();

            //modelBuilder.Entity<Client>()
            //    .Property(e => e.Gender)
            //    .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }

        public BeautySalonDB(DbContextOptions<BeautySalonDB> options) : base(options)
        {

        }
    }
}
