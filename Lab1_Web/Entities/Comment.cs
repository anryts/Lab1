namespace Lab1_Web.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public Guid ArticleId { get; set; }

    public User Author { get; set; } = null!;
    public Article Article { get; set; } = null!;
}
