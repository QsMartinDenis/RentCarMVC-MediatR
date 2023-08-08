using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Brands.Models;

namespace RentCarMVC.Features.Brands.Commands
{
    public record CreateBrandCommand(BrandViewModel ViewModel) : IRequest<bool>;

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, bool>
    {
        private readonly DataContext _dataContext;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "BrandCache";
        public CreateBrandCommandHandler(DataContext dataContext, IMemoryCache cache)
        {
            _dataContext = dataContext;
            _cache = cache;
        }

        public async Task<bool> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var model = new Brand()
            {
                BrandName = request.ViewModel.BrandName,
            };

            await _dataContext.Brands.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();
            _cache.Remove(_cacheKey);

            return result > 0;
        }
    }
}
