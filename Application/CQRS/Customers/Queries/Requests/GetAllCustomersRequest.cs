using Application.CQRS.Customers.Queries.Responses;
using Common.GlobalResopnses.Generics;
using MediatR;

namespace Application.CQRS.Customers.Queries.Requests;

public record GetAllCustomersRequest:IRequest<ResponseModelPagination<GetAllCustomersResponse>>
{
    public int Limit { get; set; } = 10;
    public int Page { get; set; } = 1;

}
