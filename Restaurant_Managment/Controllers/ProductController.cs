using Application.CQRS.Categories.Queries.Requests;
using Application.CQRS.Products.Commands.Requests;
using Application.CQRS.Products.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        return Ok(await _sender.Send(request));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductsRequest request)
        => Ok(await _sender.Send(request));


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
        => Ok(await _sender.Send(new GetProductByIdRequest { Id = id }));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _sender.Send(new DeleteProductRequest { Id = id }));

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest request)
    {
        return Ok(await _sender.Send(request));
    }
}