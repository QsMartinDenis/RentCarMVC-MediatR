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
                Brands = await _dataContext.Brands.ToListAsync(),
                VehicleTypes = await _dataContext.VehicleTypes.ToListAsync(),
                Transmissions = await _dataContext.Transmissions.ToListAsync(),
                Drives = await _dataContext.Drives.ToListAsync(),
                FuelTypes = await _dataContext.FuelTypes.ToListAsync()    
            };

            return viewModel;
        }
    }
}
