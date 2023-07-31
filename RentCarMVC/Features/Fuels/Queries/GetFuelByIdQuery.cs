using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.Fuels.Queries
{
    public record GetFuelByIdQuery(byte? Id) : IRequest<FuelType?>;

    public class GetFuelByIdQueryHandler : IRequestHandler<GetFuelByIdQuery, FuelType?>
    {
        private readonly DataContext _dataContext;

        public GetFuelByIdQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<FuelType?> Handle(GetFuelByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _dataContext.FuelType.FindAsync(request.Id);

            if (model == null)
            {
                return null;
            }

            return model;
        }
    }
}
