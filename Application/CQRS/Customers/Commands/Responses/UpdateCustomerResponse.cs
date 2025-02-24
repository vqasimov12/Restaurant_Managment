
namespace Application.CQRS.Customers.Commands.Responses;

public sealed record class UpdateCustomerResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Surname { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public int CreatedBy { get; set; }

}