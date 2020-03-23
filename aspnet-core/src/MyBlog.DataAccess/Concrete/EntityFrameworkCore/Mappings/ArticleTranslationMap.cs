using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class ArticleTranslationMap : IEntityTypeConfiguration<ArticleTranslation>
    {
        public void Configure(EntityTypeBuilder<ArticleTranslation> builder)
        {
            builder.ToTable("Article_Translations", "dbo");

            builder.HasKey(e => e.Id)
                   .HasName("article_translation_pkey");

            builder.Property(e => e.Title)
                   .HasColumnName("title")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.ContentSummary)
                   .HasColumnName("content_summary")
                   .HasMaxLength(1024)
                   .IsRequired();

            builder.Property(e => e.ContentMain)
                   .HasColumnName("content_main")
                   .IsRequired();

            builder.Property(e => e.ArticleId)
                   .HasColumnName("article_id")
                   .IsRequired();

            builder.Property(e => e.LanguageId)
                   .HasColumnName("language_id")
                   .IsRequired();

            builder.HasOne(d => d.Article)
                   .WithMany(p => p.ArticleTranslations)
                   .HasForeignKey(d => d.ArticleId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("article_translation_article_id_fkey");

            builder.HasOne(d => d.Language)
                   .WithMany(p => p.ArticleTranslations)
                   .HasForeignKey(d => d.LanguageId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("article_translation_language_id_fkey");

            builder.HasIndex(e => e.ArticleId)
                   .HasName("article_translation_ix_article_id");

            builder.HasIndex(e => e.LanguageId)
                   .HasName("article_translation_ix_language_id");
        }
    }
}
