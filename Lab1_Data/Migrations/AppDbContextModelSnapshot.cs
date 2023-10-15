﻿// <auto-generated />
using System;
using Lab1_Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lab1_Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("Lab1_Data.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("TEXT")
                        .HasColumnName("author_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("content");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_articles");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_articles_author_id");

                    b.ToTable("articles", (string)null);
                });

            modelBuilder.Entity("Lab1_Data.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("ArticleId")
                        .HasColumnType("TEXT")
                        .HasColumnName("article_id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("TEXT")
                        .HasColumnName("author_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("content");

                    b.HasKey("Id", "ArticleId")
                        .HasName("pk_comments");

                    b.HasIndex("ArticleId")
                        .HasDatabaseName("ix_comments_article_id");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_comments_author_id");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("Lab1_Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Lab1_Data.Entities.Article", b =>
                {
                    b.HasOne("Lab1_Data.Entities.User", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_articles_users_author_id");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Lab1_Data.Entities.Comment", b =>
                {
                    b.HasOne("Lab1_Data.Entities.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_comments_articles_article_id");

                    b.HasOne("Lab1_Data.Entities.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_comments_users_author_id");

                    b.Navigation("Article");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Lab1_Data.Entities.Article", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Lab1_Data.Entities.User", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
