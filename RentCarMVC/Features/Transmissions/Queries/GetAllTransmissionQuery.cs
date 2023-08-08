using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Transmissions.Models;

namespace RentCarMVC.Features.Transmissions.Queries
{
    public record GetAllTransmissionQuery : IRequest<IEnumerable<TransmissionViewModel>>;

    public class GetAllTransmissionQueryHandler : IRequestHandler<GetAllTransmissionQuery, IEnumerable<TransmissionViewModel>>
    {
        private readonly DataContext _dataContext;

        public GetAllTransmissionQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<TransmissionViewModel>> Handle(GetAllTransmissionQuery request, CancellationToken cancellationToken)
        {
            var trasmission = await _dataContext.Transmissions.ToListAsync();

            var viewModel = trasmission.Select(x => new TransmissionViewModel()
            {
                Id = x.Id,
                TransmissionName = x.TransmissionName

            });

            return viewModel;
        }
    }
}
