using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RentCarMVC.Entities;
using RentCarMVC.Features.Brands.Queries;
using RentCarMVC.Features.Cars.Commands;
using RentCarMVC.Features.Cars.Models;
using RentCarMVC.Features.Cars.Queries;

namespace RentCarMVC.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class CarController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CarController> _logger;
        private readonly string _cacheKey = "CarCache";


        public CarController(IMediator mediator,
                             IMemoryCache cache,
                             ILogger<CarController> logger)
        {
            _mediator = mediator;
            _cache = cache;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (_cache.TryGetValue(_cacheKey, out IEnumerable<CarViewModel> cars))
            {
                _logger.Log(LogLevel.Information, "Cars FOUND in cache");
            }
            else
            {
                _logger.Log(LogLevel.Information, "Cars NOT found in cache");

                cars = await _mediator.Send(new GetAllCarQuery());

                var cacheOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45)) // dupa 45 secunde se sterge cache daca iesi de pe pagina
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set(_cacheKey, cars, cacheOption);
            }

            if (cars == null && cars.Count() == 0)
            {
                return View();
            }

            return View(cars);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = await _mediator.Send(new GetAllCarDetailsQuery());
            _cache.Remove(_cacheKey);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarDetailsViewModel carViewModel)
        {
            var itemsToRemove = new List<string> { "Brands", "VehicleTypes", "Transmissions", "Drives", "FuelTypes", "Image" };

            foreach (var item in itemsToRemove)
            {
                ModelState.Remove(item);
            }

            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateCarCommand(carViewModel));

                return RedirectToAction("Index");
            }

            var model = await _mediator.Send(new GetAllCarDetailsQuery());

            carViewModel.Brands = model.Brands;
            carViewModel.VehicleTypes = model.VehicleTypes;
            carViewModel.Transmissions = model.Transmissions;
            carViewModel.Drives = model.Drives;
            carViewModel.FuelTypes = model.FuelTypes;

            return View(carViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _mediator.Send(new GetCarByIdQuery(id));

            if (model == null)
            {
                return NotFound();
            }

            var viewModel = await _mediator.Send(new GetAllCarDetailsQuery());
            viewModel.Id = model.Id;
            viewModel.CarName = model.CarName;
            viewModel.Year = model.Year;
            viewModel.PricePerDay = model.PricePerDay;
            viewModel.BrandId = model.BrandId;
            viewModel.VehicleTypeId = model.VehicleTypeId;
            viewModel.TransmissionId = model.TransmissionId;
            viewModel.DriveId = model.DriveId;
            viewModel.FuelTypeId = model.FuelTypeId;
            viewModel.Image = model.Image;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CarDetailsViewModel carDetailsViewModel)
        {
            if (id != carDetailsViewModel.Id)
            {
                return NotFound();
            }

            var itemsToRemove = new List<string> { "Brands", "VehicleTypes", "Transmissions", "Drives", "FuelTypes", "Image", "formFile" };

            foreach (var item in itemsToRemove)
            {
                ModelState.Remove(item);
            }

            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateCarCommand(carDetailsViewModel)); 

                return RedirectToAction("Index");
            }

            var modelView = await _mediator.Send(new GetAllCarDetailsQuery());

            carDetailsViewModel.Brands = modelView.Brands;
            carDetailsViewModel.VehicleTypes = modelView.VehicleTypes;
            carDetailsViewModel.Transmissions = modelView.Transmissions;
            carDetailsViewModel.Drives = modelView.Drives;
            carDetailsViewModel.FuelTypes = modelView.FuelTypes;
            _cache.Remove(_cacheKey);
            return View(carDetailsViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _mediator.Send(new GetCarByIdQuery(id));

            if (car == null)
            {
                return NotFound();
            }

            var viewModel = new CarViewModel(){
                Id = car.Id,
                CarName = car.CarName,
                BrandId = car.BrandId,  
                VehicleTypeId = car.VehicleTypeId,
                TransmissionId = car.TransmissionId,
                DriveId = car.DriveId,
                FuelTypeId = car.FuelTypeId,
                Image = car.Image
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _mediator.Send(new GetCarByIdQuery(id));

            if (model != null)
            {
                await _mediator.Send(new DeleteCarCommand(model));
                _cache.Remove(_cacheKey);
            }

            return RedirectToAction("Index");
        }
    }   
}
