using Application.CQRS.Customers.Commands.Requests;
using Application.CQRS.Customers.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCustomersRequest request)
        => Ok(await _sender.Send(request));

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerRequest request) => Ok(await _sender.Send(request));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById( int id)
    {
        var request = new GetCustomerByIdRequest() { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var request = new DeleteCustomerRequest() { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCustomerRequest request) => Ok(await _sender.Send(request));

}