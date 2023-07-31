using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Roles.Commands
{
    public record DeleteRoleCommand(IdentityRole Role) : IRequest<bool>;

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly DataContext _dataContext;

        public DeleteRoleCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Roles.Remove(request.Role);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
