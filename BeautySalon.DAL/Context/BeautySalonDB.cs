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

        public BeautySalonDB(DbContextOptions<BeautySalonDB> options) : base(options)
        {

        }
    }
}
