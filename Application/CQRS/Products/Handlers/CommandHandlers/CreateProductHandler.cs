using Application.CQRS.Products.Commands.Requests;
using Application.CQRS.Products.Commands.Responses;
using AutoMapper;
using Common.GlobalResopnses.Generics;
using Domain.Entites;
using FluentValidation;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers.CommandHandlers;

public class CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateProductRequest> validator) : IRequestHandler<CreateProductRequest, ResponseModel<CreateProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateProductRequest> _validator = validator;

    public async Task<ResponseModel<CreateProductResponse>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {

        var mappedRequest = _mapper.Map<Product>(request);

        await _unitOfWork.ProductRepository.AddAsync(mappedRequest);

        var response = _mapper.Map<CreateProductResponse>(mappedRequest);

        return new ResponseModel<CreateProductResponse>
        {
            Data = response,
            Errors = [],
            IsSuccess = true
        };
    }
}
