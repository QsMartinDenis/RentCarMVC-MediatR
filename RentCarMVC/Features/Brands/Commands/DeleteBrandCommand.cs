using MediatR;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Brands.Commands
{
    public record DeleteBrandCommand(Brand Brand) : IRequest<bool>;

    public class DeleteBrandCammandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly DataContext _dataContext;

        public DeleteBrandCammandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Brands.Remove(request.Brand);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
