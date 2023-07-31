using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.Cars.Commands
{
    public record DeleteCarCommand(Car Car) : IRequest<bool>;

    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, bool>
    {
        private readonly DataContext _dataContext;

        public DeleteCarCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Car.Remove(request.Car);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
