using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarMVC.Features.Account.Models;

namespace RentCarMVC.Features.Account.Commands
{
    public record LoginCommand(LoginViewModel User) : IRequest<SignInResult>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, SignInResult>
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginCommandHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.User.Email, request.User.Password, request.User.RememberMe, false);

            return result;
        }
    }
}
