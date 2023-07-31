using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.DriveTypes.Models;

namespace RentCarMVC.Features.DriveTypes.Commands
{
    public record CreateDriveCommand(DriveViewModel ViewModel) : IRequest<bool>;

    public class CreateDriveCommandHandler : IRequestHandler<CreateDriveCommand, bool>
    {
        private readonly DataContext _dataContext;

        public CreateDriveCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(CreateDriveCommand request, CancellationToken cancellationToken)
        {
            var model = new Drive()
            {
                DriveName = request.ViewModel.DriveName,
            };

            await _dataContext.Drive.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
