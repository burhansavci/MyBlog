using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles", "dbo");
            
            builder.HasKey(e => e.Id)
                   .HasName("id");

            builder.Property(e => e.PublishDate)
                   .HasColumnName("publish_date")
                   .IsRequired();

            builder.Property(e => e.ViewCount)
                   .HasColumnName("view_count")
                   .IsRequired();

            builder.Property(e => e.UserId)
                   .HasColumnName("user_id")
                   .IsRequired();

            builder.Property(e => e.CategoryId)
                   .HasColumnName("category_id")
                   .IsRequired();

            builder.HasOne(d => d.Category)
                   .WithMany(p => p.Articles)
                   .HasForeignKey(d => d.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("article_category_id_fkey");

            builder.HasOne(d => d.User)
                   .WithMany(p => p.Articles)
                   .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("article_user_id_fkey");

            builder.HasIndex(e => e.CategoryId)
                   .HasName("article_ix_category_id");

            builder.HasIndex(e => e.UserId)
                   .HasName("article_ix_user_id");
        }
    }
}
