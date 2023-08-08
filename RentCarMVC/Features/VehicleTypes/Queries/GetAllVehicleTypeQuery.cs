using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Features.VehicleTypes.Models;

namespace RentCarMVC.Features.VehicleTypes.Queries
{
    public record GetAllVehicleTypeQuery : IRequest<IEnumerable<VehicleTypeViewModel>>;
    
    public class GetAllVehicleTypeQueryHandler : IRequestHandler<GetAllVehicleTypeQuery, IEnumerable<VehicleTypeViewModel>>
    {
        private readonly DataContext _dataContext;

        public GetAllVehicleTypeQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<VehicleTypeViewModel>> Handle(GetAllVehicleTypeQuery request, CancellationToken cancellationToken)
        {
            var vehicleTypes = await _dataContext.VehicleTypes.ToListAsync();

            var viewModel = vehicleTypes.Select(x => new VehicleTypeViewModel()
            {
                Id = x.Id,
                TypeName = x.TypeName,
                Seats = x.Seats
            });

            return viewModel;
        }
    }
}
