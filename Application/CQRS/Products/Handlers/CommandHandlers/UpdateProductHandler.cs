using Application.CQRS.Products.Commands.Requests;
using Application.CQRS.Products.Commands.Responses;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResopnses.Generics;
using Domain.Entites;
using FluentValidation;
using MediatR;
using Repository.Common;
using System.ComponentModel.DataAnnotations;

public class UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateProductRequest> validator) : IRequestHandler<UpdateProductRequest, ResponseModel<UpdateProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateProductRequest> _validator = validator;


    public async Task<ResponseModel<UpdateProductResponse>> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
        if (product is null)
            throw new NotFoundException(typeof(Product), request.Id);

        var newProduct = _mapper.Map<Product>(request);
        newProduct.UpdatedDate = DateTime.Now;
        newProduct.UpdatedBy = 0;
        await _unitOfWork.ProductRepository.Update(newProduct);

        return new ResponseModel<UpdateProductResponse>
        {
            Data = _mapper.Map<UpdateProductResponse>(newProduct),
            Errors = [],
            IsSuccess = true
        };
    }
}
