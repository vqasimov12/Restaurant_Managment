using Application.CQRS.Products.Commands.Requests;
using Application.CQRS.Products.Commands.Responses;
using Common.Exceptions;
using Common.GlobalResopnses.Generics;
using Domain.Entites;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers.CommandHandlers;

public class DeleteProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductRequest, ResponseModel<DeleteProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseModel<DeleteProductResponse>> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var isDeleted = await _unitOfWork.ProductRepository.Remove(request.Id, 0);
        if (!isDeleted)
            throw new NotFoundException(typeof(Product), request.Id);

        return new ResponseModel<DeleteProductResponse>
        {
            Data = new DeleteProductResponse { Message = "Deleted Successfully!" },
            Errors = [],
            IsSuccess = true
        };
    }
}
