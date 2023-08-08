using MediatR;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Transmissions.Commands
{
    public record DeleteTransmissionCommand(Transmission Transmission) : IRequest<bool>;

    class DeleteTransmissionCommandHandler : IRequestHandler<DeleteTransmissionCommand, bool>
    {
        private readonly DataContext _dataContext;

        public DeleteTransmissionCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(DeleteTransmissionCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Transmissions.Remove(request.Transmission);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
