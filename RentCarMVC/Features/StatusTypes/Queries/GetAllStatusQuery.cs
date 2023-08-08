using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Features.StatusTypes.Models;

namespace RentCarMVC.Features.StatusTypes.Queries
{
    public record GetAllStatusQuery : IRequest<IEnumerable<StatusViewModel>>;

    public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, IEnumerable<StatusViewModel>>
    {
        private readonly DataContext _dataContext;

        public GetAllStatusQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<StatusViewModel>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
        {
            var status = await _dataContext.Statuses.ToListAsync();

            var viewModel = new List<StatusViewModel>();

            foreach (var model in status)
            {
                viewModel.Add(new StatusViewModel()
                {
                    Id = model.Id,
                    StatusName = model.StatusName
                });
            }

            return viewModel;
        }
    }
}
