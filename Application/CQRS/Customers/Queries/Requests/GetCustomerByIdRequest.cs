using Application.CQRS.Customers.Queries.Responses;
using Common.GlobalResopnses.Generics;
using MediatR;

namespace Application.CQRS.Customers.Queries.Requests;
public record GetCustomerByIdRequest : IRequest<ResponseModel<GetCustomerByIdResponse>>
{
    public int Id { get; set; }
}
