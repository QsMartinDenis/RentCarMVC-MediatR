using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using System.Runtime.CompilerServices;

namespace RentCarMVC.Features.Transmissions.Commands
{
    public record UpdateTransmissionCommand(Transmission Transmission) : IRequest<bool>;

    public class UpdateTransmissionCommandHandler : IRequestHandler<UpdateTransmissionCommand, bool>
    {
        private readonly DataContext _dataContext;

        public UpdateTransmissionCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(UpdateTransmissionCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Transmission.Update(request.Transmission);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
