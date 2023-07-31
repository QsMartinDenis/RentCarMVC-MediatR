using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Users.Queries
{
    public record GetByIdUserQuery(string? Id) : IRequest<IdentityUser?>;

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, IdentityUser?>
    {
        private readonly DataContext _dataContext;

        public GetByIdUserQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IdentityUser?> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.Users.FindAsync(request.Id);
        }
    }
}
