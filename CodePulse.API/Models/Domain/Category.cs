namespace CodePulse.API.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UrlHandle { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public ICollection<BlogPost>? BlogPosts { get; set; }
    }
}
