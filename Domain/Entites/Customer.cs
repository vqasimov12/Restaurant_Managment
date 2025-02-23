using Domain.BaseEntites;

namespace Domain.Entites;

public class Customer:BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Surname { get; set; }
}