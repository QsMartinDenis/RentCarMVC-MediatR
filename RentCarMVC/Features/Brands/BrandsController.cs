using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Memory;
using RentCarMVC.Features.Brands.Commands;
using RentCarMVC.Features.Brands.Models;
using RentCarMVC.Features.Brands.Queries;
using System.Drawing;

namespace RentCarMVC.Features.Brands
{
    [Authorize(Roles = "Admin")]
    public class BrandsController : Controller
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var brandViewModel = await _mediator.Send(new GetAllBrandQuery());

            return View(brandViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandViewModel brandViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateBrandCommand(brandViewModel));

                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(brandViewModel);
        }

        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _mediator.Send(new GetBrandByIdQuery(id));

            if (model == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, BrandViewModel brandViewModel)
        {
            if (id != brandViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetBrandByIdQuery(id));

                if (model == null)
                {
                    return NotFound();
                }

                model.BrandName = brandViewModel.BrandName;

                var result = await _mediator.Send(new UpdateBrandCommand(model));

                if (!result)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(brandViewModel);
        }

        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _mediator.Send(new GetBrandByIdQuery(id));

            if (brand == null)
            {
                return NotFound();
            }

            var viewModel = new BrandViewModel()
            {
                Id = brand.Id,
                BrandName = brand.BrandName,
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var model = await _mediator.Send(new GetBrandByIdQuery(id));

            if (model != null)
            {
                await _mediator.Send(new DeleteBrandCommand(model));
            }

            return RedirectToAction("Index");
        }
    }
}
