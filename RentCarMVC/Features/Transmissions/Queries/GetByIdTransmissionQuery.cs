using MediatR;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Transmissions.Queries
{
    public record GetByIdTransmissionQuery(byte? Id) : IRequest<Transmission?>;

    public class GetByIdTransmissionQueryHandler : IRequestHandler<GetByIdTransmissionQuery, Transmission?>
    {
        private readonly DataContext _dataContext;

        public GetByIdTransmissionQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Transmission?> Handle(GetByIdTransmissionQuery request, CancellationToken cancellationToken)
        {
            var model = await _dataContext.Transmissions.FindAsync(request.Id);

            if (model == null)
            {
                return null;
            }

            return model;
        }
    }
}
