using Application.CQRS.Categories.Queries.Responses;
using Application.CQRS.Products.Queries.Requests;
using Application.CQRS.Products.Queries.Responses;
using AutoMapper;
using Common.GlobalResopnses.Generics;
using MediatR;
using Repository.Common;
using Repository.Repositories;

namespace Application.CQRS.Products.Handlers.QueryHandlers;

public class GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllProductsRequest, ResponseModelPagination<GetAllProductsResponse>>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModelPagination<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        var products = _unitOfWork.ProductRepository.GetAll();

        if (!products.Any())
            return new ResponseModelPagination<GetAllProductsResponse>() { Data = null, Errors = [], IsSuccess = true };

        var totalCount = products.Count();
        products = products.Skip((request.Page - 1) * request.Limit).Take(request.Limit);
        var mappedProducts = new List<GetAllProductsResponse>();
        foreach (var product in products)
            mappedProducts.Add(_mapper.Map<GetAllProductsResponse>(product));

        var response = new Pagination<GetAllProductsResponse>() { Data = mappedProducts, TotalDataCount = totalCount };
        return new ResponseModelPagination<GetAllProductsResponse>
        {
            Data = response,
            Errors = [],
        };
    }
}
