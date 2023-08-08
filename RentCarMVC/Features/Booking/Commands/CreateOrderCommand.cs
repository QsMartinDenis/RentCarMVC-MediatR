using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.Booking.Commands
{
    public record CreateOrderCommand(BookingOrder BookingOrder) : IRequest<bool>;

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly DataContext _dataContext;

        public CreateOrderCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _dataContext.BookingOrders.AddAsync(request.BookingOrder);
            var result =  await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
