using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.VehicleTypes.Models;

namespace RentCarMVC.Features.VehicleTypes.Commands
{
    public record CreateVehicleTypeCommand(VehicleTypeViewModel ViewModel) : IRequest<bool>;

    public class CreateVehicleTypeCommandHandler : IRequestHandler<CreateVehicleTypeCommand, bool>
    {
        private readonly DataContext _dataContext;

        public CreateVehicleTypeCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(CreateVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            var model = new VehicleType()
            {
                TypeName = request.ViewModel.TypeName,
                Seats = request.ViewModel.Seats,
            };

            await _dataContext.VehicleTypes.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
