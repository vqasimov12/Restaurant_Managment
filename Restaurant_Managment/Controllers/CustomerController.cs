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
}