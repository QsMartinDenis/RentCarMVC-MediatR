using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Cars.Models;

namespace RentCarMVC.Features.Cars.Queries
{
    public record GetAllCarQuery : IRequest<IEnumerable<CarViewModel>>;
    
    public class GetAllCarQueryHandler : IRequestHandler<GetAllCarQuery, IEnumerable<CarViewModel>>
    {
        private readonly DataContext _dataContext;

        public GetAllCarQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<CarViewModel>> Handle(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            var cars = await _dataContext.Cars
                                         .Include(c => c.Brand)
                                         .Include(c => c.VehicleType)
                                         .Include(c => c.Transmission)
                                         .Include(c => c.Drive)
                                         .Include(c => c.FuelType)
                                         .ToListAsync();


            var viewModel = new List<CarViewModel>();

            foreach (var car in cars)
            {
                viewModel.Add(new CarViewModel()
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
                    Image = car.Image,
                });
            }

            return viewModel;
        }
    }
}
