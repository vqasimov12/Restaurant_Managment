using Application.CQRS.Products.Queries.Requests;
using Application.CQRS.Products.Queries.Responses;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResopnses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers.QueryHandlers;

public class GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProductByIdRequest, ResponseModel<GetProductByIdResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModel<GetProductByIdResponse>> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

        if (product is null)
            throw new BadRequestException("Product can not be found");

        var mappedProduct = _mapper.Map<GetProductByIdResponse>(product);
        
        return new ResponseModel<GetProductByIdResponse>
        {
            Data = mappedProduct,
            Errors = [],
            IsSuccess = true
        };
    }
}
