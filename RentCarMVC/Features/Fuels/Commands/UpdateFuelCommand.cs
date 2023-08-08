using MediatR;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Fuels.Commands
{
    public record UpdateFuelCommand(FuelType Fuel) : IRequest<bool>;

    public class UpdateFuelCommandHandler : IRequestHandler<UpdateFuelCommand, bool>
    {
        private readonly DataContext _dataContext;

        public UpdateFuelCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> Handle(UpdateFuelCommand request, CancellationToken cancellationToken)
        {
            _dataContext.FuelTypes.Update(request.Fuel);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
