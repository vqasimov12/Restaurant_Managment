using Application.CQRS.Customers.Queries.Requests;
using Application.CQRS.Customers.Queries.Responses;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResopnses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Customers.Handlers.QueryHandlers;

public class GetCustomerByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCustomerByIdRequest, ResponseModel<GetCustomerByIdResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModel<GetCustomerByIdResponse>> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);

        if (customer is null)
            throw new BadRequestException("Customer can not be found with provided id");

        var mappedCustomer = _mapper.Map<GetCustomerByIdResponse>(customer);

        return new ResponseModel<GetCustomerByIdResponse>
        {
            Data = mappedCustomer,
            Errors = [],
            IsSuccess = true
        };
    }
}
