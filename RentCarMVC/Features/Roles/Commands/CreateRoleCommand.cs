using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Roles.Models;

namespace RentCarMVC.Features.Roles.Commands
{
    public record CreateRoleCommand(RoleViewModel ViewModel) : IRequest<bool>;

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly DataContext _dataContext;

        public CreateRoleCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var model = new IdentityRole()
            {
                Name = request.ViewModel.Role,
            };

            await _dataContext.Roles.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
