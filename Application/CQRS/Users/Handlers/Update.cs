using Application.CQRS.Users.DTOs;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResopnses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;

public class Update
{
    public record struct Command : IRequest<ResponseModel<UpdateDto>>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Command, ResponseModel<UpdateDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseModel<UpdateDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.UserRepository.GetByIdAsync(request.Id).Result;
            if (user is null)
                throw new BadRequestException("User does not exist with provided id ");
            user.Phone = request.Phone;
            user.Email = request.Email;
            user.Name = request.Name;
            user.Surname = request.Surname;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChanges();

            var response = _mapper.Map<UpdateDto>(user);
            return new ResponseModel<UpdateDto>()
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };


        }
    }

}
