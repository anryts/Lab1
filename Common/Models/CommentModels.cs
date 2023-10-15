using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models;

public record CommentCreationModel (Guid AuthorId, Guid ArticleId, string Message);
public record CommentUpdateModel(Guid Id, string Message);
public record CommentDeleteModel(Guid Id);
public record CommentGetForArticle(Guid ArticleId);
public record CommentGetForAuthor(Guid AuthorId);

