using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.Cars.Models;

namespace RentCarMVC.Features.Cars.Commands
{
    public record CreateCarCommand(CarDetailsViewModel ViewModel) : IRequest<bool>;

    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, bool>
    {
        private readonly DataContext _dataContext;

        public CreateCarCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var model = new Car()
            {
                CarName = request.ViewModel.CarName,
                Year = request.ViewModel.Year,
                PricePerDay = request.ViewModel.PricePerDay,
                BrandId = request.ViewModel.BrandId,
                VehicleTypeId = request.ViewModel.VehicleTypeId,
                TransmissionId = request.ViewModel.TransmissionId,
                DriveId = request.ViewModel.DriveId,
                FuelTypeId = request.ViewModel.FuelTypeId,
            };

            if (request.ViewModel.formFile != null && request.ViewModel.formFile.Length > 0)
            {
                model.Image = ImageToBytes(request.ViewModel.formFile);
            }

            await _dataContext.Cars.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

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
