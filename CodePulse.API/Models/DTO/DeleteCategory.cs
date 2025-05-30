namespace CodePulse.API.Models.DTO
{
    public class DeleteCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UrlHandle { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
