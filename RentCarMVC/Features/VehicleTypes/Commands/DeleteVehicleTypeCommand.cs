using MediatR;
using RentCarMVC.Data;

namespace RentCarMVC.Features.VehicleTypes.Commands
{
    public record DeleteVehicleTypeCommand(VehicleType Vehicle) : IRequest<bool>;

    public class DeleteVehicleTypeCommandHandler : IRequestHandler<DeleteVehicleTypeCommand, bool>
    {
        private readonly DataContext _dataContext;

        public DeleteVehicleTypeCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(DeleteVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            _dataContext.VehicleTypes.Remove(request.Vehicle);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
