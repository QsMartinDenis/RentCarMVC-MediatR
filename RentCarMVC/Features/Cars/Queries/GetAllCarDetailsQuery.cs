using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Features.Cars.Models;

namespace RentCarMVC.Features.Cars.Queries
{
    public record GetAllCarDetailsQuery : IRequest<CarDetailsViewModel>;

    public class GetAllCarDetailsQueryHandler : IRequestHandler<GetAllCarDetailsQuery, CarDetailsViewModel>
    {
        private readonly DataContext _dataContext;

        public GetAllCarDetailsQueryHandler(DataContext dataContext, IMediator mediator)
        {
            _dataContext = dataContext;
        }

        public async Task<CarDetailsViewModel> Handle(GetAllCarDetailsQuery request, CancellationToken cancellationToken)
        {
            var viewModel = new CarDetailsViewModel()
            {
                Brands = await _dataContext.Brand.ToListAsync(),
                VehicleTypes = await _dataContext.VehicleType.ToListAsync(),
                Transmissions = await _dataContext.Transmission.ToListAsync(),
                Drives = await _dataContext.Drive.ToListAsync(),
                FuelTypes = await _dataContext.FuelType.ToListAsync()    
            };

            return viewModel;
        }
    }
}
