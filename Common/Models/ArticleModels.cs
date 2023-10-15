using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models;

public record ArticleCreationModel(string Title, string Content, Guid AuthorId);
public record ArticleDeleteModel(Guid Id);
public record ArticleUpdateModel(Guid Id, string? Title, string? Content);
public record ArticleGetForAuthor(Guid AuthorId);

