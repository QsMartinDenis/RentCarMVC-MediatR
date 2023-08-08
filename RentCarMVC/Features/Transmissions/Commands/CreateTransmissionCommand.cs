using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Transmissions.Models;

namespace RentCarMVC.Features.Transmissions.Commands
{
    public record CreateTransmissionCommand(TransmissionViewModel ViewModel) : IRequest<bool>;

    public class CreateTransmissionCommandHandler : IRequestHandler<CreateTransmissionCommand, bool>
    {
        private readonly DataContext _dataContext;

        public CreateTransmissionCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(CreateTransmissionCommand request, CancellationToken cancellationToken)
        {
            var model = new Transmission()
            {
                TransmissionName = request.ViewModel.TransmissionName,
            };

            await _dataContext.Transmissions.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
