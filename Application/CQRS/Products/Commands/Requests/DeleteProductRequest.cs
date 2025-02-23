using Application.CQRS.Products.Commands.Responses;
using Common.GlobalResopnses.Generics;
using MediatR;

namespace Application.CQRS.Products.Commands.Requests;

public class DeleteProductRequest:IRequest<ResponseModel<DeleteProductResponse>>
{
    public int Id { get; set; }
}
