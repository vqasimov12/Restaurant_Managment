using Common.Exceptions;
using Common.GlobalResopnses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;

public class Delete
{
public record struct Command(int Id) : IRequest<ResponseModel<Unit>>
    {
        public int Id { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command, ResponseModel<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponseModel<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (user is null)
                throw new BadRequestException("User can not be found with provided id");
            _unitOfWork.UserRepository.Remove(request.Id);
           await _unitOfWork.SaveChanges();
           return new ResponseModel<Unit>
           {
               Data = Unit.Value,
               Errors = [],
               IsSuccess = true
           };
        }
    }
}
