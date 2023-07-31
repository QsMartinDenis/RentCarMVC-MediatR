using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarMVC.Data;
using RentCarMVC.Features.Account.Models;

namespace RentCarMVC.Features.Account.Commands
{
    public record RegisterCommand(RegisterViewModel Model) : IRequest<IdentityResult>;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IdentityResult>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly DataContext _dataContext;

        public RegisterCommandHandler(UserManager<IdentityUser> userManager,
                                      SignInManager<IdentityUser> signInManager,
                                      DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
        }

        public async Task<IdentityResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser
            {
                UserName = request.Model.UserName,
                Email = request.Model.Email,
            };

            var result = await _userManager.CreateAsync(user, request.Model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                await _dataContext.Users.AddAsync(user);
            }

            return result;
        }
    }
}
