using Application.CQRS.Customers.Commands.Requests;
using Application.CQRS.Customers.Commands.Responses;
using AutoMapper;
using Common.GlobalResopnses.Generics;
using FluentValidation;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Customers.Handlers.CommandHandlers;

public class CreateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateCustomerRequest> validator) : IRequestHandler<CreateCustomerRequest, ResponseModel<CreateCustomerResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateCustomerRequest> _validator = validator;

    public Task<ResponseModel<CreateCustomerResponse>> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
