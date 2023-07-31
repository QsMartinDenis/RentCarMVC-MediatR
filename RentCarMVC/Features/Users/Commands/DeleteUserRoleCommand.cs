using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Users.Commands
{
    public record DeleteUserRoleCommand(IdentityUserRole<string> UserRole) : IRequest<bool>;

    public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand, bool>
    {
        private readonly DataContext _dataContext;

        public DeleteUserRoleCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            _dataContext.UserRoles.Remove(request.UserRole);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
