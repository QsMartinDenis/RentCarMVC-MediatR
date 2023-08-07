using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarMVC.Features.DriveTypes.Commands;
using RentCarMVC.Features.DriveTypes.Models;
using RentCarMVC.Features.DriveTypes.Queries;

namespace RentCarMVC.Features.DriveTypes
{
    [Authorize(Roles = "Admin")]
    public class DriveController : Controller
    {
        private readonly IMediator _mediator;

        public DriveController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var drive = await _mediator.Send(new GetAllDriveQuery());

            if (drive == null)
            {
                return View();
            }

            return View(drive);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DriveViewModel driveViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateDriveCommand(driveViewModel));

                if (result)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }

            return View(driveViewModel);
        }

        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _mediator.Send(new GetDriveByIdQuery(id));

            if (model == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, DriveViewModel driveViewModel)
        {
            if (id != driveViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetDriveByIdQuery(id));

                if (model == null)
                {
                    return NotFound();
                }
                model.DriveName = driveViewModel.DriveName;

                await _mediator.Send(new UpdateDriveCommand(model));

                return RedirectToAction(nameof(Index));
            }
            return View(driveViewModel);
        }

        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drive = await _mediator.Send(new GetDriveByIdQuery(id));

            if (drive == null)
            {
                return NotFound();
            }

            var viewModel = new DriveViewModel()
            {
                Id = drive.Id,
                DriveName = drive.DriveName,
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var model = await _mediator.Send(new GetDriveByIdQuery(id));

            if (model != null)
            {
                await _mediator.Send(new DeleteDriveCommand(model));
            }

            return RedirectToAction("Index");
        }
    }
}
