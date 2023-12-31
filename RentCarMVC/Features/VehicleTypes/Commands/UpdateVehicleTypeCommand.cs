﻿using MediatR;
using RentCarMVC.Data;

namespace RentCarMVC.Features.VehicleTypes.Commands
{
    public record UpdateVehicleTypeCommand(VehicleType Vehicle) : IRequest<bool>;

    public class UpdateVehicleTypeCommandHandler : IRequestHandler<UpdateVehicleTypeCommand, bool>
    {
        private readonly DataContext _dataContext;

        public UpdateVehicleTypeCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(UpdateVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            _dataContext.VehicleTypes.Update(request.Vehicle);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
