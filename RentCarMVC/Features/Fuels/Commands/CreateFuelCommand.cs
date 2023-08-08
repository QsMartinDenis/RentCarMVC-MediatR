using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Fuels.Models;

namespace RentCarMVC.Features.Fuels.Commands
{
    public record CreateFuelCommand(FuelTypeViewModel ViewModel) : IRequest<bool>;

    public class CreateFuelCommandHandler : IRequestHandler<CreateFuelCommand, bool>
    {
        private readonly DataContext _dataContext;

        public CreateFuelCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(CreateFuelCommand request, CancellationToken cancellationToken)
        {
            var model = new FuelType()
            {
                FuelName = request.ViewModel.FuelName,
            };

            await _dataContext.FuelTypes.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
