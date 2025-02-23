using Application.CQRS.Categories.Commands.Responses;
using Common.GlobalResopnses.Generics;
using MediatR;

namespace Application.CQRS.Categories.Commands.Requests;

public record struct UpdateCategoryRequest:IRequest<ResponseModel<UpdateCategoryResponse>>
{
    public int Id {  get; set; }
    public string Name { get; set; }
}
