using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentCarMVC.Data;
using RentCarMVC.Features.Fuels.Models;

namespace RentCarMVC.Features.Fuels.Queries
{
    public record GetAllFuelQuery : IRequest<IEnumerable<FuelTypeViewModel>>;

    public class GetAllFuelQueryHandler : IRequestHandler<GetAllFuelQuery, IEnumerable<FuelTypeViewModel>>
    {
        private readonly DataContext _dataContext;

        public GetAllFuelQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<FuelTypeViewModel>> Handle(GetAllFuelQuery request, CancellationToken cancellationToken)
        {
            var fuels = await _dataContext.FuelTypes.ToListAsync();

            var viewModel = fuels.Select(x => new FuelTypeViewModel()
            {
                Id = x.Id,
                FuelName = x.FuelName
            });

            return viewModel;
        }
    }
}
