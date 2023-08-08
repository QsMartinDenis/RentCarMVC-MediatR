using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Brands.Commands
{
    public record DeleteBrandCommand(Brand Brand) : IRequest<bool>;

    public class DeleteBrandCammandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly DataContext _dataContext;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "BrandCache";
        public DeleteBrandCammandHandler(DataContext dataContext, IMemoryCache cache)
        {
            _dataContext = dataContext;
            _cache = cache;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Brands.Remove(request.Brand);
            var result = await _dataContext.SaveChangesAsync();
            _cache.Remove(_cacheKey);

            return result > 0;
        }
    }
}
