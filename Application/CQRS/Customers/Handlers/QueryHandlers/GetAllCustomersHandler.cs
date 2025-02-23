using Application.CQRS.Customers.Queries.Requests;
using Application.CQRS.Customers.Queries.Responses;
using Application.CQRS.Products.Queries.Responses;
using AutoMapper;
using Common.GlobalResopnses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Customers.Handlers.QueryHandlers;

public class GetAllCustomersHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllCustomersRequest, ResponseModelPagination<GetAllCustomersResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModelPagination<GetAllCustomersResponse>> Handle(GetAllCustomersRequest request, CancellationToken cancellationToken)
    {
        var customers = _unitOfWork.CustomerRepository.GetAll();

        if (!customers.Any())
            return new ResponseModelPagination<GetAllCustomersResponse>() { Data = null, Errors = [], IsSuccess = true };

        var totalCount = customers.Count();
        customers = customers.Skip((request.Page - 1) * request.Limit).Take(request.Limit);
        var mappedCustomers = new List<GetAllCustomersResponse>();
        foreach (var customer in customers)
            mappedCustomers.Add(_mapper.Map<GetAllCustomersResponse>(customer));

        var response = new Pagination<GetAllCustomersResponse>() { Data = mappedCustomers, TotalDataCount = totalCount };
        return new ResponseModelPagination<GetAllCustomersResponse>
        {
            Data = response,
            Errors = [],
        };
    }
}
