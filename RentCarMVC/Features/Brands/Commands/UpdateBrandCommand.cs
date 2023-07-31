﻿using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.Brands.Commands
{
    public record UpdateBrandCommand(Brand Brand) : IRequest<bool>;

    public class UpdateBrandCommandHandlre : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly DataContext _dataContext;

        public UpdateBrandCommandHandlre(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Brand.Update(request.Brand);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0; 
        }
    }
}
