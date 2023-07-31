using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.Cars.Queries
{
    public record GetCarByIdQuery(int? Id) : IRequest<Car?>;

    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, Car?>
    {
        private readonly DataContext _dataContext;

        public GetCarByIdQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Car?> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _dataContext.Car.FindAsync(request.Id);

            return model == null ? null : model;
        }
    }
}
