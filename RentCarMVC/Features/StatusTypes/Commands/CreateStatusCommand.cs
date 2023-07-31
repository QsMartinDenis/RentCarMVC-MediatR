using MediatR;
using RentCarMVC.Data;
using RentCarMVC.Entities;
using RentCarMVC.Features.StatusTypes.Models;

namespace RentCarMVC.Features.StatusTypes.Commands
{
    public record CreateStatusCommand(StatusViewModel ViewModel) : IRequest<bool>;

    public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand,bool>
    {
        private readonly DataContext _dataContext;

        public CreateStatusCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            var model = new Status()
            {
                StatusName = request.ViewModel.StatusName,
            };

            await _dataContext.Status.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
