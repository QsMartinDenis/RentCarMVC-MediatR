using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RentCarMVC.Data;
using RentCarMVC.Features.Brands.Models;

namespace RentCarMVC.Features.Brands.Queries
{
    public record GetAllBrandQuery : IRequest<IEnumerable<BrandViewModel>>;

    public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, IEnumerable<BrandViewModel>>
    {
        private readonly DataContext _dataContext;
        private readonly IMemoryCache _cache;
        private readonly ILogger<BrandsController> _logger;
        private readonly string _cacheKey = "BrandCache";
        public GetAllBrandQueryHandler(DataContext dataContext, IMemoryCache cache, ILogger<BrandsController> logger)
        {
            _dataContext = dataContext;
            _cache = cache;
            _logger = logger;
        }

        public async Task<IEnumerable<BrandViewModel>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {

            if (_cache.TryGetValue(_cacheKey, out IEnumerable<BrandViewModel> cache))
            {
                _logger.Log(LogLevel.Information, "Brands FOUND in cache");
                return cache;
            }
            else
            {
                _logger.Log(LogLevel.Information, "Brands NOT found in cache");

                var brands = await _dataContext.Brands.ToListAsync();

                var viewModel = brands.Select(item => new BrandViewModel
                {
                    Id = item.Id,
                    BrandName = item.BrandName
                });

                var cacheOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set(_cacheKey, viewModel, cacheOption);
                return viewModel;
            }
        }
    }
}
