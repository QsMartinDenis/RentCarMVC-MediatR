using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.Booking.Queries
{
    public record GetOrderByCarIdQuery(int CarId) : IRequest<List<BookingOrder>>;

    public class GetOrderByCarIdQueryHandler : IRequestHandler<GetOrderByCarIdQuery, List<BookingOrder>>
    {
        private readonly DataContext _dataContext;

        public GetOrderByCarIdQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<BookingOrder>> Handle(GetOrderByCarIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _dataContext.BookingOrders
                        .Where(x => x.CarId == request.CarId &&
                                    x.Status.StatusName == "In Progress" ||
                                    x.Status.StatusName == "Confirmed")
                        .OrderBy(x => x.PickupDate)
                        .ToListAsync();

            return orders;
        }
    }
}
