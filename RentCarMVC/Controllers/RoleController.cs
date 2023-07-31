using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarMVC.Features.Roles.Commands;
using RentCarMVC.Features.Roles.Models;
using RentCarMVC.Features.Roles.Queries;

namespace RentCarMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _mediator.Send(new GetAllRoleQuery());

            if (roles.Count() == 0)
            {
                return View();
            }

            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                var role = await _mediator.Send(new CreateRoleCommand(roleViewModel));

                return RedirectToAction("Index");
            }

            return View(roleViewModel);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuel = await _mediator.Send(new GetRoleByIdQuery(id));

            if (fuel == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetRoleByIdQuery(id));

                if (model == null)
                {
                    return NotFound();
                }

                model.Name = roleViewModel.Role;

                await _mediator.Send(new UpdateRoleCommand(model));

                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _mediator.Send(new GetRoleByIdQuery(id));

            if (role == null)
            {
                return NotFound();
            }

            var viewModel = new RoleViewModel()
            {
                Id = role.Id,
                Role = role.Name
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var model = await _mediator.Send(new GetRoleByIdQuery(id));

            if (model != null)
            {
                await _mediator.Send(new DeleteRoleCommand(model));
            }

            return RedirectToAction("Index");
        }
    }
}
