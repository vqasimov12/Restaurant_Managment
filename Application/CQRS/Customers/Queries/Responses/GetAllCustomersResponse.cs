namespace Application.CQRS.Customers.Queries.Responses;

public record GetAllCustomersResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string CreatedAt { get; set; }
    public DateTime CreatedDate { get; set; }

}