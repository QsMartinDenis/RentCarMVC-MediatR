using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Brands.Models;

namespace RentCarMVC.Features.Brands.Commands
{
    public record CreateBrandCommand(BrandViewModel ViewModel) : IRequest<bool>;

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, bool>
    {
        private readonly DataContext _dataContext;

        public CreateBrandCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var model = new Brand()
            {
                BrandName = request.ViewModel.BrandName,
            };

            await _dataContext.Brand.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
