using Application.CQRS.Categories.Commands.Responses;
using Common.GlobalResopnses;
using Common.GlobalResopnses.Generics;
using MediatR;

namespace Application.CQRS.Categories.Commands.Requests;

public record struct DeleteCategoryRequest:IRequest<ResponseModel<DeleteCategoryResponse>>
{
    public int Id { get; set; }
}