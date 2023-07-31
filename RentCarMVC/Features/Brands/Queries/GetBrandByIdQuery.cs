using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.Brands.Queries
{
    public record GetBrandByIdQuery(byte? Id) : IRequest<Brand?>;

    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Brand?>
    {
        private readonly DataContext _dataContext;

        public GetBrandByIdQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Brand?> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _dataContext.Brand.FindAsync(request.Id);

            if (model == null)
            {
                return null;
            }

            return model;
        }
    }
}
