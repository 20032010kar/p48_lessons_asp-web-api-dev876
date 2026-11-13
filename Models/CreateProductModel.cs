using System;

namespace p48_lessons_asp.Models;

public class CreateProductModel
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public IFormFile? productImage{get;set;}
    public Guid CategoryId { get; set; }


}
