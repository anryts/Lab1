using Common.Models;
using Lab1_Data;
using Lab1_Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab1_Web.Services;

public class ArticleService : IArticleService
{
    private readonly AppDbContext _context;

    public ArticleService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateArticle(ArticleCreationModel model)
    {
        var article = new Article
        {
            Id = Guid.NewGuid(),
            Title = model.Title,
            Content = model.Content,
            AuthorId = model.AuthorId,
        };

        await _context.Articles.AddAsync(article);
        await _context.SaveChangesAsync();
        return article.Id;
    }

    public async Task DeleteArticle(ArticleDeleteModel model)
    {
        var article= await _context.Articles
            .FirstOrDefaultAsync(article => article.Id == model.Id)
            ?? throw new Exception("Article not found");

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Article>> GetAllArticles()
    {
        return await _context.Articles.ToListAsync();
    }

    public async Task<List<Article>> GetArticleForAuthor(ArticleGetForAuthor model)
    {
        var articles = await _context.Articles
            .Where(article => article.AuthorId == model.AuthorId)
            .ToListAsync();
        return articles;
    }

    public async Task UpdateArticle(ArticleUpdateModel model)
    {
        throw new NotImplementedException();
    }

}
