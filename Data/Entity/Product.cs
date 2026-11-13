namespace p48_lessons_asp.Data.Entity
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty; 
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false; 
    }
}