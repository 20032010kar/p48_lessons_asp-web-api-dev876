namespace p48_lessons_asp.Models;

public class UpdateProductModel
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
}