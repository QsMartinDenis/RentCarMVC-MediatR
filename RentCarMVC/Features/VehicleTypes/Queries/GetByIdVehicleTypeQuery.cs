using MediatR;
using RentCarMVC.Data;

namespace RentCarMVC.Features.VehicleTypes.Queries
{
    public record GetByIdVehicleTypeQuery(byte? Id) : IRequest<VehicleType?>;

    public class GetByIdVehicleTypeQueryHandler : IRequestHandler<GetByIdVehicleTypeQuery, VehicleType?>
    {
        private readonly DataContext _dataContext;

        public GetByIdVehicleTypeQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<VehicleType?> Handle(GetByIdVehicleTypeQuery request, CancellationToken cancellationToken)
        {
            var model = await _dataContext.VehicleTypes.FindAsync(request.Id);

            if (model == null)
            {
                return null;
            }

            return model;
        }
    }
}
