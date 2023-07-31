using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Features.Roles.Models;

namespace RentCarMVC.Features.Users.Queries
{
    public record GetByIdRoleQuery(RoleViewModel RoleViewModel) : IRequest<string?>;

    public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, string?>
    {
        private readonly DataContext _dataContext;

        public GetByIdRoleQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<string?> Handle(GetByIdRoleQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.Roles.Where(r => r.Name == request.RoleViewModel.Role)
                                           .Select(r => r.Id)
                                           .FirstOrDefaultAsync(); 
        }
    }
}
