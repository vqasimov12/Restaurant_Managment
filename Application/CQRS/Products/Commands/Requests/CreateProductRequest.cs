using Application.CQRS.Products.Commands.Responses;
using Common.GlobalResopnses.Generics;
using MediatR;

namespace Application.CQRS.Products.Commands.Requests;

public class CreateProductRequest:IRequest<ResponseModel<CreateProductResponse>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
}