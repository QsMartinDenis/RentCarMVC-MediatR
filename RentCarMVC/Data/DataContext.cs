using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Entities;

namespace RentCarMVC.Data
{
    public class DataContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public DataContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Drive> Drive { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<FuelType> FuelType { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Transmission> Transmission { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<BookingOrder> BookingOrder { get; set; }
    }
}
