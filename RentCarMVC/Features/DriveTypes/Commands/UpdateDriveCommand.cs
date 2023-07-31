using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.DriveTypes.Commands
{
    public record UpdateDriveCommand(Drive Drive) : IRequest<bool>;

    public class UpdateDriveCommandHandler : IRequestHandler<UpdateDriveCommand, bool>
    {
        private readonly DataContext _dataContext;

        public UpdateDriveCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(UpdateDriveCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Drive.Update(request.Drive);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
