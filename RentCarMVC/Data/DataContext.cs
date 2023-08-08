using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Entities;
using RentCarMVC.Features.Brands;
using RentCarMVC.Features.Cars;
using RentCarMVC.Features.DriveTypes;
using RentCarMVC.Features.Fuels;
using RentCarMVC.Features.StatusTypes;
using RentCarMVC.Features.Transmissions;
using RentCarMVC.Features.VehicleTypes;

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
        public virtual DbSet<Drive> Drives { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<FuelType> FuelTypes { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Transmission> Transmissions { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<BookingOrder> BookingOrders { get; set; }
    }
}
