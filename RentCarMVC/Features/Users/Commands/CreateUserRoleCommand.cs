using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Users.Commands
{
    public record CreateUserRoleCommand(string UserId, string RoleId) : IRequest<bool>;

    public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, bool>
    {
        private readonly DataContext _dataContext;

        public CreateUserRoleCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = new IdentityUserRole<string>()
            {
                UserId = request.UserId,
                RoleId = request.RoleId
            };

            await _dataContext.UserRoles.AddAsync(userRole);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
