using Common.Models;
using Lab1_Data.Entities;

namespace Lab1_Web.Services;

public interface ICommentService
{
    Task<List<Comment>> GetAllComments();
    Task<Guid> CreateComment(CommentCreationModel model);
    Task DeleteComment(CommentDeleteModel model);
    Task UpdateComment(CommentUpdateModel model);
    Task<List<Comment>> GetCommentsForArticle(CommentGetForArticle model);
    Task<List<Comment>> GetCommentsForAuthor(CommentGetForAuthor model);
}
