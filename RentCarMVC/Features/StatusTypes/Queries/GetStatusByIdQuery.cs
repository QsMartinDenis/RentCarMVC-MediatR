using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.StatusTypes.Queries
{
    public record GetStatusByIdQuery(byte? Id) : IRequest<Status?>;

    public class GetStatusByIdQueryHandler : IRequestHandler<GetStatusByIdQuery, Status?>
    {
        private readonly DataContext _dataContext;

        public GetStatusByIdQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Status?> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _dataContext.Status.FindAsync(request.Id);

            if (model == null)
            {
                return null;
            }

            return model;
        }
    }
}
