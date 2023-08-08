using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Booking.Models;

namespace RentCarMVC.Features.Booking.Queries
{
    public record GetUserOrderByIdQuery(string Id) : IRequest<IEnumerable<OrderViewModel?>>;
    
    public class GetUserOrderByIdQueryHandler : IRequestHandler<GetUserOrderByIdQuery, IEnumerable<OrderViewModel?>>
    {
        private readonly DataContext _dataContext;

        public GetUserOrderByIdQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<OrderViewModel?>> Handle(GetUserOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _dataContext.BookingOrders
                               .Include(c => c.Car)
                               .Include(c => c.Car.Brand)
                               .Include(c => c.Car.VehicleType)
                               .Include(c => c.Car.Transmission)
                               .Include(c => c.Car.Drive)
                               .Include(c => c.Car.FuelType)
                               .Where(x => x.User.Id == request.Id)
                               .ToListAsync();

            if (orders.Count == 0)
            {
                return Enumerable.Empty<OrderViewModel>();
            }

            var viewModel = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                viewModel.Add(new OrderViewModel()
                {
                    Id = order.Id,
                    Car = order.Car,
                    PickupDate = order.PickupDate,
                    ReturnDate = order.ReturnDate,
                    TotalAmount = order.TotalAmount,
                });
            }
            return viewModel;
        }
    }
}
