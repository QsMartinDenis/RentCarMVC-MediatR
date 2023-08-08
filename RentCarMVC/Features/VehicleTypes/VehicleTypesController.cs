using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarMVC.Features.VehicleTypes.Commands;
using RentCarMVC.Features.VehicleTypes.Models;
using RentCarMVC.Features.VehicleTypes.Queries;

namespace RentCarMVC.Features.VehicleTypes
{
    [Authorize(Roles = "Admin")]
    public class VehicleTypesController : Controller
    {
        private readonly IMediator _mediator;

        public VehicleTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var vehicleType = await _mediator.Send(new GetAllVehicleTypeQuery());

            if (vehicleType == null)
            {
                return View();
            }

            return View(vehicleType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleTypeViewModel vehicleTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateVehicleTypeCommand(vehicleTypeViewModel));

                return RedirectToAction("Index");
            }

            return View(vehicleTypeViewModel);
        }

        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _mediator.Send(new GetByIdVehicleTypeQuery(id));

            if (model == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, VehicleTypeViewModel vehicleTypeViewModel)
        {
            if (id != vehicleTypeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetByIdVehicleTypeQuery(id));

                if (model == null)
                {
                    return NotFound();
                }

                model.TypeName = vehicleTypeViewModel.TypeName;
                model.Seats = vehicleTypeViewModel.Seats;

                await _mediator.Send(new UpdateVehicleTypeCommand(model));

                return RedirectToAction(nameof(Index));
            }
            return View(vehicleTypeViewModel);
        }

        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleType = await _mediator.Send(new GetByIdVehicleTypeQuery(id));

            if (vehicleType == null)
            {
                return NotFound();
            }

            var viewModel = new VehicleTypeViewModel()
            {
                Id = vehicleType.Id,
                TypeName = vehicleType.TypeName,
                Seats = vehicleType.Seats
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var model = await _mediator.Send(new GetByIdVehicleTypeQuery(id));

            if (model != null)
            {
                await _mediator.Send(new DeleteVehicleTypeCommand(model));
            }

            return RedirectToAction("Index");
        }
    }
}
