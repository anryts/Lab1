using Common.Models;
using Lab1_Web.Entities;

namespace Lab3.Services;

public interface ICommentService
{
    Task<List<Comment>> GetAllComments();
    Task<Guid> CreateComment(CommentCreationModel model);
    Task DeleteComment(CommentDeleteModel model);
    Task UpdateComment(CommentUpdateModel model);
    Task<List<Comment>> GetCommentsForArticle(CommentGetForArticle model);
    Task<List<Comment>> GetCommentsForAuthor(CommentGetForAuthor model);
}
