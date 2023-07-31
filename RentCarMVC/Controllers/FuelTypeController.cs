using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarMVC.Entities;
using RentCarMVC.Features.Fuels.Commands;
using RentCarMVC.Features.Fuels.Models;
using RentCarMVC.Features.Fuels.Queries;

namespace RentCarMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FuelTypeController : Controller
    {
        private readonly IMediator _mediator;
        public FuelTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var fuelTypes = await _mediator.Send(new GetAllFuelQuery());

            if (fuelTypes == null)
            {
                return View();
            }

            return View(fuelTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuelTypeViewModel fuelTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var model = new FuelType()
                {
                    FuelName = fuelTypeViewModel.FuelName
                };

                await _mediator.Send(new CreateFuelCommand(fuelTypeViewModel));

                return RedirectToAction("Index");
            }

            return View(fuelTypeViewModel);
        }

        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuel = await _mediator.Send(new GetFuelByIdQuery(id));

            if (fuel == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, FuelTypeViewModel fuelTypeViewModel)
        {
            if (id != fuelTypeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetFuelByIdQuery(id));

                if (model == null)
                {
                    return NotFound();
                }

                model.FuelName = fuelTypeViewModel.FuelName;

                await _mediator.Send(new UpdateFuelCommand(model));

                return RedirectToAction(nameof(Index));
            }
            return View(fuelTypeViewModel);
        }

        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelType = await _mediator.Send(new GetFuelByIdQuery(id));

            if (fuelType == null)
            {
                return NotFound();
            }

            var viewModel = new FuelTypeViewModel()
            {
                Id = fuelType.Id,
                FuelName = fuelType.FuelName
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var model = await _mediator.Send(new GetFuelByIdQuery(id));

            if (model != null)
            {
                await _mediator.Send(new DeleteFuelCommand(model));
            }
           
            return RedirectToAction("Index");
        }
    }
}
