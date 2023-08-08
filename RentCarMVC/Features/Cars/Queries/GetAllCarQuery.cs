using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Cars.Models;

namespace RentCarMVC.Features.Cars.Queries
{
    public record GetAllCarQuery : IRequest<IEnumerable<CarViewModel>>;
    
    public class GetAllCarQueryHandler : IRequestHandler<GetAllCarQuery, IEnumerable<CarViewModel>>
    {
        private readonly DataContext _dataContext;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CarsController> _logger;
        private readonly string _cacheKey = "CarCache";

        public GetAllCarQueryHandler(DataContext dataContext, ILogger<CarsController> logger, IMemoryCache cache)
        {
            _dataContext = dataContext;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IEnumerable<CarViewModel>> Handle(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(_cacheKey, out IEnumerable<CarViewModel> cache))
            {
                _logger.Log(LogLevel.Information, "Cars FOUND in cache");
                return cache;
            }
            else
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
                _logger.Log(LogLevel.Information, "Cars NOT found in cache");

                var cacheOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45)) // dupa 45 secunde se sterge cache daca iesi de pe pagina
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set(_cacheKey, viewModel, cacheOption);
                return viewModel;
            }
        }
    }
}
