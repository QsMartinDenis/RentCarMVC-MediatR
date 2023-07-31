using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Features.Booking.Models;
using RentCarMVC.Features.Cars.Models;

namespace RentCarMVC.Features.Cars.Queries
{
    public record CarFilterQuery(Filter Filter) : IRequest<IEnumerable<CarViewModel>>;

    public class CarFilterQueryHandler : IRequestHandler<CarFilterQuery, IEnumerable<CarViewModel>>
    {
        private readonly DataContext _dataContext;

        public CarFilterQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<CarViewModel>> Handle(CarFilterQuery request, CancellationToken cancellationToken)
        {
            var carsQuery = _dataContext.Car
                                        .Where(c => request.Filter.Brand == null || request.Filter.Brand.Contains(c.Brand.BrandName))
                                        .Where(c => request.Filter.VehicleType == null || request.Filter.VehicleType.Contains(c.VehicleType.TypeName))
                                        .Where(c => request.Filter.Transmission == null || request.Filter.Transmission.Contains(c.Transmission.TransmissionName))
                                        .Where(c => request.Filter.Drive == null || request.Filter.Drive.Contains(c.Drive.DriveName))
                                        .Where(c => request.Filter.FuelType == null || request.Filter.FuelType.Contains(c.FuelType.FuelName));

            switch (request.Filter.OrderBy)
            {
                case "year_asc":
                    carsQuery = carsQuery.OrderBy(s => s.Year);
                    break;
                case "year_desc":
                    carsQuery = carsQuery.OrderByDescending(s => s.Year);
                    break;
                case "price_asc":
                    carsQuery = carsQuery.OrderBy(s => s.PricePerDay);
                    break;
                case "price_desc":
                    carsQuery = carsQuery.OrderByDescending(s => s.PricePerDay);
                    break;
            }

            var carsList = await carsQuery
                .Include(c => c.Brand)
                .Include(c => c.VehicleType)
                .Include(c => c.Transmission)
                .Include(c => c.Drive)
                .Include(c => c.FuelType)
                .ToListAsync();

            var viewModels = new List<CarViewModel>();

            foreach (var car in carsList)
            {
                viewModels.Add(new CarViewModel()
                {
                    Id = car.Id,
                    CarName = car.CarName,
                    Year = car.Year,
                    PricePerDay = car.PricePerDay,
                    Brand = car.Brand,
                    VehicleType = car.VehicleType,
                    Transmission = car.Transmission,
                    Drive = car.Drive,
                    FuelType = car.FuelType,
                    Image = car.Image
                });
            }

            return viewModels;
        }
    }
}
