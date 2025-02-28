using Application.CQRS.Customers.Commands.Responses;
using Common.GlobalResopnses.Generics;
using MediatR;

namespace Application.CQRS.Customers.Commands.Requests;

public record struct CreateCustomerRequest : IRequest<ResponseModel<CreateCustomerResponse>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}