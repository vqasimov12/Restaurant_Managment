namespace Application.CQRS.Customers.Commands.Responses;

public record CreateCustomerResponse
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}