using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Cars.Queries
{
    public record GetAllCarByIdQuery(int Id) : IRequest<Car?>;

    public class GetAllCarByIdQueryHandler : IRequestHandler<GetAllCarByIdQuery, Car?>
    {
        private readonly DataContext _dataContext;

        public GetAllCarByIdQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Car?> Handle(GetAllCarByIdQuery request, CancellationToken cancellationToken)
        {
            var car = await _dataContext.Cars
                                    .Include(c => c.Brand)
                                    .Include(c => c.Drive)
                                    .Include(c => c.FuelType)
                                    .Include(c => c.Transmission)
                                    .Include(c => c.VehicleType)
                                    .Where(c => c.Id == request.Id)
                                    .FirstOrDefaultAsync();

            return car;

            //De ce acest cod returneaza null, dar prin var returneaza valoarea corecta

            /*return await _dataContext.Car
                                    .Include(c => c.Brand)
                                    .Include(c => c.Drive)
                                    .Include(c => c.FuelType)
                                    .Include(c => c.Transmission)
                                    .Include(c => c.VehicleType)
                                    .Where(c => c.Id == request.Id)
                                    .FirstOrDefaultAsync();*/         
        }
    }
}
