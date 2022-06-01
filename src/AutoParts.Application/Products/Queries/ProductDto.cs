namespace AutoParts.Application.Products.Queries;

public class ProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? EAN { get; set; }
    public string? Image { get; set; }
    public int Count { get; set; }
    public string? Category { get; set; }
}