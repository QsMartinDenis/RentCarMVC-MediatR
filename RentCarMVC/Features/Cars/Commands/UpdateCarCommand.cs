using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Cars.Models;

namespace RentCarMVC.Features.Cars.Commands
{
    public record UpdateCarCommand(CarDetailsViewModel ViewModel) : IRequest<bool>;

    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, bool>
    {
        private readonly DataContext _dataContext;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "CarCache";

        public UpdateCarCommandHandler(DataContext dataContext, IMemoryCache cache)
        {
            _dataContext = dataContext;
            _cache = cache;
        }

        public async Task<bool> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var model = await _dataContext.Cars.FindAsync(request.ViewModel.Id);

            if (model == null)
            {
                return false;
            }

            model.CarName = request.ViewModel.CarName;
            model.PricePerDay = request.ViewModel.PricePerDay;
            model.Year = request.ViewModel.Year;
            model.BrandId = request.ViewModel.BrandId;
            model.VehicleTypeId = request.ViewModel.VehicleTypeId;
            model.TransmissionId = request.ViewModel.TransmissionId;
            model.DriveId = request.ViewModel.DriveId;
            model.FuelTypeId = request.ViewModel.FuelTypeId;

            if (request.ViewModel.formFile != null && request.ViewModel.formFile.Length > 0)
            {
                model.Image = ImageToBytes(request.ViewModel.formFile);
            }

            _dataContext.Cars.Update(model);
            var result = await _dataContext.SaveChangesAsync();
            _cache.Remove(_cacheKey);

            return result > 0;
        }
        private byte[] ImageToBytes(IFormFile formFile)
        {
            using (var stream = new MemoryStream())
            {
                formFile.CopyTo(stream);
                return stream.ToArray();
            }
        }
    }
}
