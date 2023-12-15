using Common.Models;
using Lab1_Web.Entities;

namespace Lab1_Web.Services;

public interface IArticleService
{
    Task<Guid> CreateArticle(ArticleCreationModel model);
    Task DeleteArticle(ArticleDeleteModel model);
    Task UpdateArticle(ArticleUpdateModel model);
    Task<List<Article>> GetAllArticles();
    Task<List<Article>> GetArticleForAuthor(ArticleGetForAuthor model);
}
