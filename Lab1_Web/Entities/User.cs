namespace Lab1_Web.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Guid? GenderId { get; set; }

    public Gender? Gender { get; set; }
    public List<Article> Articles { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}
