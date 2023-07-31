using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarMVC.Data;

namespace RentCarMVC.Features.Roles.Commands
{
    public record UpdateRoleCommand(IdentityRole Role) : IRequest<bool>;

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly DataContext _dataContext;

        public UpdateRoleCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Roles.Update(request.Role);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
