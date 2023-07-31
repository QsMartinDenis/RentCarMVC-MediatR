using MediatR;
using Microsoft.AspNetCore.Identity;

namespace RentCarMVC.Features.Account.Commands
{
    public record LogoutCommand : IRequest<Task>;

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Task>
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutCommandHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Task> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();

            return Task.CompletedTask;
        }
    }
}
