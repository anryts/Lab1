using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1_Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUnusedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_author");

            migrationBuilder.CreateIndex(
                name: "ix_articles_author_id",
                table: "articles",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "fk_articles_users_author_id",
                table: "articles",
                column: "author_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_articles_users_author_id",
                table: "articles");

            migrationBuilder.DropIndex(
                name: "ix_articles_author_id",
                table: "articles");

            migrationBuilder.CreateTable(
                name: "article_author",
                columns: table => new
                {
                    author_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    article_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article_author", x => new { x.author_id, x.article_id });
                    table.ForeignKey(
                        name: "fk_article_author_articles_article_id",
                        column: x => x.article_id,
                        principalTable: "articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_article_author_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_article_author_article_id",
                table: "article_author",
                column: "article_id");
        }
    }
}
