using Common.Models;
using Dapper;
using Lab1_Data.Entities;
using Lab1_Web.Abstraction;

namespace Lab1_Web.Services;

public class CommentService : ICommentService
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public CommentService(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<List<Comment>> GetAllComments()
    {
        await using var connection = _sqlConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var command = new CommandDefinition(
            "SELECT * FROM Comments");
        var comments = await connection.QueryAsync<Comment>(command);
        return comments.ToList();
    }

    public async Task<Guid> CreateComment(CommentCreationModel model)
    {
        await using var connection = _sqlConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Content = model.Message,
            AuthorId = model.AuthorId,
            ArticleId = model.ArticleId
        };
        var command = new CommandDefinition(
            "INSERT INTO Comments (Id, Message, AuthorId, ArticleId) " +
            "VALUES (@Id, @Message, @AuthorId, @ArticleId)",
            comment);
        await connection.ExecuteAsync(command);
        return comment.Id;
    }

    public async Task DeleteComment(CommentDeleteModel model)
    {
        await using var connection = _sqlConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var command = new CommandDefinition(
            "DELETE FROM Comments WHERE Id = @Id",
            new { model.Id });
        await connection.ExecuteAsync(command);
    }

    public async Task UpdateComment(CommentUpdateModel model)
    {
        await using var connection = _sqlConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var command = new CommandDefinition(
            "UPDATE Comments SET Message = @Message WHERE Id = @Id",
            new { model.Id, model.Message });
    }

    public async Task<List<Comment>> GetCommentsForArticle(CommentGetForArticle model)
    {
        await using var connection = _sqlConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var command = new CommandDefinition(
            "SELECT * FROM Comments WHERE ArticleId = @ArticleId",
            new { ArticleId = model.ArticleId });
        var comments = await connection.QueryAsync<Comment>(command);
        return comments.ToList();
    }

    public async Task<List<Comment>> GetCommentsForAuthor(CommentGetForAuthor model)
    {
        await using var connection = _sqlConnectionFactory.CreateConnection();
        await connection.OpenAsync();
        var command = new CommandDefinition(
            "SELECT * FROM Comments WHERE AuthorId = @AuthorId",
            new { AuthorId = model.AuthorId });
        var comments = await connection.QueryAsync<Comment>(command);
        return comments.ToList();
    }
}