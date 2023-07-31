using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Users.Queries
{
    public record GetByIdUserRoleQuery(string UserId, string RoleId) : IRequest<IdentityUserRole<string>?>;

    public class GetByIdUserRoleQueryHandler : IRequestHandler<GetByIdUserRoleQuery, IdentityUserRole<string>?>
    {
        private readonly DataContext _dataContext;

        public GetByIdUserRoleQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IdentityUserRole<string>?> Handle(GetByIdUserRoleQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.UserRoles.Where(x => x.UserId == request.UserId && x.RoleId == request.RoleId)
                                               .FirstOrDefaultAsync();
        }
    }
}
