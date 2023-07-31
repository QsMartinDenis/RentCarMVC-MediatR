using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Roles.Queries
{
    public record GetRoleByIdQuery(string? Id) : IRequest<IdentityRole?>;

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, IdentityRole?>
    {
        private readonly DataContext _dataContext;

        public GetRoleByIdQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IdentityRole?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _dataContext.Roles.FindAsync(request.Id);

            if (model == null)
            {
                return null;
            }

            return model;
        }
    }
}
