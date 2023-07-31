using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarMVC.Entities;
using RentCarMVC.Features.StatusTypes.Commands;
using RentCarMVC.Features.StatusTypes.Models;
using RentCarMVC.Features.StatusTypes.Queries;
using System.Data;

namespace RentCarMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatusController : Controller
    {
        private readonly IMediator _mediator;

        public StatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _mediator.Send(new GetAllStatusQuery());

            if (viewModel == null)
            {
                return View();
            }

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StatusViewModel statusViewModel)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateStatusCommand(statusViewModel));

                return RedirectToAction("Index");
            }

            return View(statusViewModel);
        }

        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _mediator.Send(new GetStatusByIdQuery(id));

            if (model == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, StatusViewModel statusViewModel)
        {
            if (id != statusViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetStatusByIdQuery(id));

                if (model == null)
                {
                    return NotFound();
                }

                model.StatusName = statusViewModel.StatusName;

                await _mediator.Send(new UpdateStatusCommand(model));

                return RedirectToAction(nameof(Index));
            }
            return View(statusViewModel);
        }

        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _mediator.Send(new GetStatusByIdQuery(id));

            if (status == null)
            {
                return NotFound();
            }

            var viewModel = new StatusViewModel()
            {
                Id = status.Id,
                StatusName = status.StatusName,
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var model = await _mediator.Send(new GetStatusByIdQuery(id));

            if (model != null)
            {
                await _mediator.Send(new DeleteStatusCommand(model));
            }

            return RedirectToAction("Index");
        }
    }
}
