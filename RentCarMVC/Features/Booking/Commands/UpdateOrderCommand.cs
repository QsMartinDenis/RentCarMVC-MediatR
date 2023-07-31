using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.Booking.Commands
{
    public record UpdateOrderCommand(BookingOrder Order) : IRequest<bool>;

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly DataContext _dataContext;

        public UpdateOrderCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            _dataContext.BookingOrder.Update(request.Order);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
