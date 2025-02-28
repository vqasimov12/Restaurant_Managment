using Application.CQRS.Customers.Commands.Requests;
using Application.CQRS.Customers.Commands.Responses;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResopnses.Generics;
using FluentValidation;
using MediatR;
using Repository.Common;
using System.Security.AccessControl;

namespace Application.CQRS.Customers.Handlers.CommandHandlers;

public sealed class UpdateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateCustomerRequest> validator) : IRequestHandler<UpdateCustomerRequest, ResponseModel<UpdateCustomerResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateCustomerRequest> _validator = validator;

    public async Task<ResponseModel<UpdateCustomerResponse>> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);
        if (customer == null)
            throw new BadRequestException("Customer can not be found with provided id");

        customer.Name = request.Name;
        customer.Surname = request.Surname;
        customer.Email = request.Email;
        customer.Phone = request.Phone;
        customer.Address = request.Address;
        customer.UpdatedDate = DateTime.Now;
        await _unitOfWork.CustomerRepository.Update(customer);
        await _unitOfWork.SaveChanges();



        return new ResponseModel<UpdateCustomerResponse>()
        {
            Data = _mapper.Map<UpdateCustomerResponse>(customer),
            Errors = [],
            IsSuccess = true
        };
    }
}
