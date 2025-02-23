using Application.CQRS.Categories.Commands.Requests;
using Application.CQRS.Categories.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequest request)
        => Ok(await _sender.Send(request));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var request = new GetByIdCategoryRequest() { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCategoryRequest request)
        => Ok(await _sender.Send(request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var request = new DeleteCategoryRequest() { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, string Name)
    {
        var request=new UpdateCategoryRequest() { Id = id, Name = Name };
        return Ok(await _sender.Send(request));
    }

}
