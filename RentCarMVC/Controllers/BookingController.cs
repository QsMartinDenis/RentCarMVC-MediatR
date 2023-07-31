using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarMVC.Entities;
using RentCarMVC.Features.Booking.Commands;
using RentCarMVC.Features.Booking.Models;
using RentCarMVC.Features.Booking.Queries;
using RentCarMVC.Features.Cars.Queries;
using RentCarMVC.Features.StatusTypes.Queries;
using RentCarMVC.Features.Users.Queries;
using System.Security.Claims;

namespace RentCarMVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly IMediator _mediator;
        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _mediator.Send(new GetAllCarQuery());

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Filters(Filter filter)
        {

            if (!filter.IsValid())
            {
                return RedirectToAction("Index");
            }

            var viewModel = await _mediator.Send(new CarFilterQuery(filter));
            
            return View("Index", viewModel);
        }
       
        public async Task<IActionResult> Booking(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Register", "Account");
            }

            var car = await _mediator.Send(new GetAllCarByIdQuery(id));

            if (car == null)
            {
                return NotFound();
            }

            var orderViewModel = new OrderViewModel() {
                CarId = car.Id,
                Car = car,
            };

            return View(orderViewModel);
        }

        [HttpPost, ActionName("Confirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookingConfirm(OrderViewModel orderViewModel)
        {
            var car = await _mediator.Send(new GetAllCarByIdQuery(orderViewModel.Id));

            if (car == null)
            {
                return NotFound();
            }

            bool isValid = true;
            var days = orderViewModel.ReturnDate - orderViewModel.PickupDate;

            if (days.Days == 0)
            {
                isValid = false;
                ModelState.AddModelError(string.Empty, $"If you want to take and return the car on the same day select {orderViewModel.PickupDate:dd-MM-yyyy} and " +
                                                       $"{orderViewModel.ReturnDate.AddDays(1):dd-MM-yyyy}");
            }
            else if (orderViewModel.PickupDate < DateTime.Now.Date) 
            {
                isValid = false;
                ModelState.AddModelError(string.Empty, "Pick up date >= DateTime.Now");
            }
            else if (orderViewModel.PickupDate > orderViewModel.ReturnDate)
            {
                isValid = false;
                ModelState.AddModelError(string.Empty, "Pick up date <= Return Date");
            }

            if (!isValid)
            {
                orderViewModel.Car = car;
                return View("Booking", orderViewModel);
            }

            var orders = await _mediator.Send(new GetOrderByCarIdQuery(orderViewModel.CarId));

            if (orders.Count() == 1)
            {
                if (!(orderViewModel.PickupDate >= orders[0].ReturnDate || orderViewModel.ReturnDate <= orders[0].PickupDate))
                {
                    ModelState.AddModelError(string.Empty, "This car is not available");
                    orderViewModel.Car = car;
                    return View("Booking", orderViewModel);
                }
            }
            else if (orders.Count() > 1)
            {
                bool isAvailable = false;

                for (int i = 0; i < orders.Count(); i++)
                {
                    var nextOrder = orders.ElementAtOrDefault(i + 1);

                    if (nextOrder != null)
                    {
                        if (orderViewModel.PickupDate >= orders[i].ReturnDate && orderViewModel.ReturnDate <= nextOrder.PickupDate)
                        {
                            isAvailable = true;
                            break;
                        }
                    }
                    else if (nextOrder == null && orderViewModel.PickupDate >= orders.Last().ReturnDate || orderViewModel.ReturnDate <= orders[0].PickupDate)
                    {
                        isAvailable = true;
                    }
                }

                if (!isAvailable)
                {
                    ModelState.AddModelError(string.Empty, "This car is not available");
                    orderViewModel.Car = car;
                    return View("Booking", orderViewModel);
                }
            }

            var price = days.Days * car.PricePerDay;
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _mediator.Send(new GetByIdUserQuery(userId));

            var bookingOreder = new BookingOrder()
            {
                User = user,
                CarId = orderViewModel.CarId,
                PickupDate = orderViewModel.PickupDate,
                ReturnDate = orderViewModel.ReturnDate,
                TotalAmount = price,
                StatusId =  1
            };

            await _mediator.Send(new CreateOrderCommand(bookingOreder));

            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> UserOrders()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = await _mediator.Send(new GetUserOrderByIdQuery(userId));

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminOrders()
        {
            var viewModel = await _mediator.Send(new GetAllBookingQuery());

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Confirm(int id, string status)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));

            if (order == null)
            {
                return NotFound();
            }

            var statusId = (await _mediator.Send(new GetAllStatusQuery()))
                                                  .Where(x => x.StatusName == status)
                                                  .Select(x => x.Id)
                                                  .FirstOrDefault();

            if (statusId == 0)
            {
                return NotFound();
            }

            order.StatusId = statusId;

            await _mediator.Send(new UpdateOrderCommand(order));

            return RedirectToAction("AdminOrders");
        }
    }
}
 