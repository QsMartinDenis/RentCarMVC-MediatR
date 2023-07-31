using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.Fuels.Commands
{
    public record DeleteFuelCommand(FuelType Fuel) : IRequest<bool>;

    public class DeleteFuelCommandHandler : IRequestHandler<DeleteFuelCommand, bool>
    {
        private readonly DataContext _dataContext;

        public DeleteFuelCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(DeleteFuelCommand request, CancellationToken cancellationToken)
        {
            _dataContext.FuelType.Remove(request.Fuel);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
