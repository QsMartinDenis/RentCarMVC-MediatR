﻿using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;

namespace RentCarMVC.Features.StatusTypes.Commands
{
    public record DeleteStatusCommand(Status Status) : IRequest<bool>;

    public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, bool>
    {
        private readonly DataContext _dataContext;

        public DeleteStatusCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Status.Remove(request.Status);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
