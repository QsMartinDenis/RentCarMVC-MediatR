using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Booking.Models;
using RentCarMVC.Features.Cars.Models;

namespace RentCarMVC.Features.Booking.Queries
{
    public record GetAllBookingQuery : IRequest<IEnumerable<OrderViewModel>>;

    public class GetAllBookingQueryHandler : IRequestHandler<GetAllBookingQuery, IEnumerable<OrderViewModel>>
    {
        private readonly DataContext _dataContext;

        public GetAllBookingQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<OrderViewModel>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
        {
            var orders = await _dataContext.BookingOrders
                               .Include(x => x.Car)
                               .Include(x => x.User)
                               .Include(x => x.Status)
                               .OrderBy(x => x.PickupDate)
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
                    UserId = order.User.Id,
                    UserName = order.User.UserName,
                    Car = order.Car,
                    PickupDate = order.PickupDate,
                    ReturnDate = order.ReturnDate,
                    TotalAmount = order.TotalAmount,
                    OrderStatus = order.Status.StatusName
                });
            }

            return viewModel;
        }
    }
}
