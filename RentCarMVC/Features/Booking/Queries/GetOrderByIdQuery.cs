using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.Booking.Queries
{
    public record GetOrderByIdQuery(int Id) : IRequest<BookingOrder?>;

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, BookingOrder?>
    {
        private DataContext _dataContext;

        public GetOrderByIdQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<BookingOrder?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.BookingOrder.Where(x => x.Id == request.Id)
                                                  .FirstOrDefaultAsync();
        }
    }
}
