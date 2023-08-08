using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Brands.Commands
{
    public record UpdateBrandCommand(Brand Brand) : IRequest<bool>;

    public class UpdateBrandCommandHandlre : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly DataContext _dataContext;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "BrandCache";
        public UpdateBrandCommandHandlre(DataContext dataContext, IMemoryCache cache)
        {
            _dataContext = dataContext;
            _cache = cache;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Brands.Update(request.Brand);
            var result = await _dataContext.SaveChangesAsync();
            _cache.Remove(_cacheKey);

            return result > 0; 
        }
    }
}
