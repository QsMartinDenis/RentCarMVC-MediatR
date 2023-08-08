using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarMVC.Features.Roles.Models;
using RentCarMVC.Features.Users.Commands;
using RentCarMVC.Features.Users.Queries;

namespace RentCarMVC.Features.Users
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _mediator.Send(new GetAllUserRoleQuery());

            if (users.Count() == 0)
            {
                return View();
            }

            return View(users);
        }

        public async Task<IActionResult> Add(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _mediator.Send(new GetByIdUserQuery(id));

            if (model == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(string id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
            {
                return NotFound();
            }

            var roleId = await _mediator.Send(new GetByIdRoleQuery(roleViewModel));

            if (roleId == null)
            {
                ModelState.AddModelError(string.Empty, "The specified role does not exist. Or role for this user exist.");
                return View(roleViewModel);
            }

            var exists = await _mediator.Send(new GetByIdUserRoleQuery(id, roleId));

            if (exists != null || roleId == null)
            {
                ModelState.AddModelError(string.Empty, "The specified role does not exist. Or role for this user exist.");
                return View(roleViewModel);
            }

            await _mediator.Send(new CreateUserRoleCommand(id, roleId));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _mediator.Send(new GetByIdUserQuery(id));

            if (model == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, RoleViewModel roleViewModel)
        {
            var roleId = await _mediator.Send(new GetByIdRoleQuery(roleViewModel));

            if (roleId == null)
            {
                ModelState.AddModelError(string.Empty, "The specified role does not exist");
                return View(roleViewModel);
            }

            var model = await _mediator.Send(new GetByIdUserRoleQuery(id, roleId));

            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Current user don't have this role");
                return View(roleViewModel);
            }

            await _mediator.Send(new DeleteUserRoleCommand(model));

            return RedirectToAction("Index");
        }
    }
}
