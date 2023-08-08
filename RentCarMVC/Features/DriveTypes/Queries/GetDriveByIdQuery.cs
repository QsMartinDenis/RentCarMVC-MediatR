using MediatR;
using RentCarMVC.Data;

namespace RentCarMVC.Features.DriveTypes.Queries
{
    public record GetDriveByIdQuery(byte? Id) : IRequest<Drive?>;

    public class GetDriveByIdQueryHandler : IRequestHandler<GetDriveByIdQuery, Drive?>
    {
        private readonly DataContext _dataContext;

        public GetDriveByIdQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Drive?> Handle(GetDriveByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _dataContext.Drives.FindAsync(request.Id);

            if (model == null)
            {
                return null;
            }

            return model;
        }
    }
}
