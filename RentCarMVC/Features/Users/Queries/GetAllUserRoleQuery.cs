using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarMVC.Data;
using RentCarMVC.Features.Users.Models;

namespace RentCarMVC.Features.Users.Queries
{
    public record GetAllUserRoleQuery : IRequest<IEnumerable<UserViewModel>>;

    public class GetAllUserRoleQueryHandler : IRequestHandler<GetAllUserRoleQuery, IEnumerable<UserViewModel>>
    {
        private readonly DataContext _dataContext;

        public GetAllUserRoleQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<UserViewModel>> Handle(GetAllUserRoleQuery request, CancellationToken cancellationToken)
        {
            var query = await (from u in _dataContext.Users
                              join ur in _dataContext.UserRoles on u.Id equals ur.UserId into userRoles
                              from ur in userRoles.DefaultIfEmpty()
                              join r in _dataContext.Roles on ur.RoleId equals r.Id into roles
                              from r in roles.DefaultIfEmpty()
                              select new
                              {
                                  UserId = u.Id,
                                  u.UserName,
                                  u.Email,
                                  RoleName = r.Name
                              })
                        .GroupBy(u => new { u.UserId, u.UserName, u.Email })
                        .Select(g => new
                        {
                            g.Key.UserId,
                            g.Key.UserName,
                            g.Key.Email,
                            Roles = g.Select(u => u.RoleName).ToList()
                        })
                        .ToListAsync();


            var userViewModels = new List<UserViewModel>();

            foreach (var user in query)
            {
                userViewModels.Add(new UserViewModel()
                {
                    Id = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Roles
                });
            }

            return userViewModels;
        }
    }
}
