using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Concrete;
using System;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments", "dbo");

            builder.HasKey(e => e.Id)
                   .HasName("comment_pkey");

            builder.Property(e => e.Name)
                   .HasColumnName("name")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.ContentMain)
                   .HasColumnName("content_main")
                   .IsRequired();

            builder.Property(e => e.PublishDate)
                   .HasColumnName("publish_date")
                   .IsRequired();

            builder.Property(e => e.ArticleId)
                   .HasColumnName("article_id")
                   .IsRequired();

            builder.Property(e => e.ParentId)
                   .HasColumnName("parent_id");

            builder.HasOne(d => d.Article)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(d => d.ArticleId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("comment_article_id_fkey");

            builder.HasOne(d => d.Parent)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(d => d.ParentId)
                   .HasConstraintName("comment_parent_id_fkey");

            builder.HasIndex(e => e.ArticleId)
                   .HasName("comment_ix_article_id");

            builder.HasIndex(e => e.ParentId)
                   .HasName("comment_ix_parent_id");
        }
    }
}
