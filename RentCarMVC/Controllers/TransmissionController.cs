using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarMVC.Features.Transmissions.Commands;
using RentCarMVC.Features.Transmissions.Models;
using RentCarMVC.Features.Transmissions.Queries;

namespace RentCarMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TransmissionController : Controller
    {
        private readonly IMediator _mediator;

        public TransmissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var transmissions = await _mediator.Send(new GetAllTransmissionQuery());

            if (transmissions == null)
            {
                return View();
            }

            return View(transmissions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransmissionViewModel transmissionViewModel)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateTransmissionCommand(transmissionViewModel));

                return RedirectToAction("Index");
            }

            return View(transmissionViewModel);
        }

        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _mediator.Send(new GetByIdTransmissionQuery(id));

            if (model == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, TransmissionViewModel transmissionViewModel)
        {
            if (id != transmissionViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetByIdTransmissionQuery(id));

                if (model == null)
                {
                    return NotFound();
                }

                model.TransmissionName = transmissionViewModel.TransmissionName;

                await _mediator.Send(new  UpdateTransmissionCommand(model));

                return RedirectToAction(nameof(Index));
            }
            return View(transmissionViewModel);
        }

        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transmission = await _mediator.Send(new GetByIdTransmissionQuery(id));

            if (transmission == null)
            {
                return NotFound();
            }

            var viewModel = new TransmissionViewModel()
            {
                Id = transmission.Id,
                TransmissionName = transmission.TransmissionName,
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var model = await _mediator.Send(new GetByIdTransmissionQuery(id));

            if (model != null)
            {
                await _mediator.Send(new DeleteTransmissionCommand(model));
            }

            return RedirectToAction("Index");
        }
    }
}
