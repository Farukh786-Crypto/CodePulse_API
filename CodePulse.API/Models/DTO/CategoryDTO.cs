namespace CodePulse.API.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UrlHandle { get; set; }
        public int? CategoryId { get; set; } = 0;
    }
}
