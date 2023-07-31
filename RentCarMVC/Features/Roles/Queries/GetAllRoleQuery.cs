using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Features.Roles.Models;
using System.Collections;

namespace RentCarMVC.Features.Roles.Queries
{
    public record GetAllRoleQuery : IRequest<IEnumerable<RoleViewModel>>;

    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, IEnumerable<RoleViewModel>>
    {
        private readonly DataContext _dataContext;

        public GetAllRoleQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<RoleViewModel>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var fuels = await _dataContext.Roles.ToListAsync();

            var viewModel = fuels.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Role = x.Name
            });

            return viewModel;
        }
    }
}
