namespace Application.CQRS.Customers.Queries.Responses;

public record GetCustomerByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    
}