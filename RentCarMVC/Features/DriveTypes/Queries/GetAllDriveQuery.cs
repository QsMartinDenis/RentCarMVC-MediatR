using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Features.DriveTypes.Models;

namespace RentCarMVC.Features.DriveTypes.Queries
{
    public record GetAllDriveQuery : IRequest<IEnumerable<DriveViewModel>>;

    public class GetAllDriveQueryHandler : IRequestHandler<GetAllDriveQuery, IEnumerable<DriveViewModel>>
    {
        private readonly DataContext _dataContext;

        public GetAllDriveQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<DriveViewModel>> Handle(GetAllDriveQuery request, CancellationToken cancellationToken)
        {
            var drive = await _dataContext.Drive.ToListAsync();

            var viewModel = drive.Select(x => new DriveViewModel()
            {
                Id = x.Id,
                DriveName = x.DriveName
            });

            return viewModel;
        }
    }
}
