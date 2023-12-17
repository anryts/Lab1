using Lab3.Entities;

namespace Lab1_Web.Entities;

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public Guid AuthorId { get; set; }

    public User Author { get; set; } = null!;
    public List<Comment> Comments { get; set; } = new();
}
