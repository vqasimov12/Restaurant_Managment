namespace Application.CQRS.Products.Commands.Responses;

public class UpdateProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
    public int UpdateBy { get; set; }
    public int CreatedBy { get; set; }
    public DateTime UpdateDate { get; set; } = DateTime.Now;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
