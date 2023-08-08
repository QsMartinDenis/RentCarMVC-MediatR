using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Cars.Commands
{
    public record DeleteCarCommand(Car Car) : IRequest<bool>;

    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, bool>
    {
        private readonly DataContext _dataContext;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "CarCache";
        public DeleteCarCommandHandler(DataContext dataContext, IMemoryCache cache)
        {
            _dataContext = dataContext;
            _cache = cache;
        }

        public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Cars.Remove(request.Car);
            var result = await _dataContext.SaveChangesAsync();
            _cache.Remove(_cacheKey);

            return result > 0;
        }
    }
}
