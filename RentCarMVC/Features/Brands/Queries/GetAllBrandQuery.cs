using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Features.Brands.Models;

namespace RentCarMVC.Features.Brands.Queries
{
    public record GetAllBrandQuery : IRequest<IEnumerable<BrandViewModel>>;

    public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, IEnumerable<BrandViewModel>>
    {
        private readonly DataContext _dataContext;

        public GetAllBrandQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<BrandViewModel>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await _dataContext.Brand.ToListAsync();

            var viewModel = brands.Select(item => new BrandViewModel
            {
                Id = item.Id,
                BrandName = item.BrandName
            });

            return viewModel;
        }
    }
}
