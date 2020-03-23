using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class CategoryTranslationMap : IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.ToTable("Category_Translations", "dbo");

            builder.HasKey(e => e.Id)
                   .HasName("category_translation_pkey");

            builder.Property(e => e.Name)
                   .HasColumnName("name")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Description)
                   .HasColumnName("description")
                   .HasMaxLength(1024);

            builder.Property(e => e.CategoryId)
                   .HasColumnName("category_id")
                   .IsRequired();

            builder.Property(e => e.LanguageId)
                   .HasColumnName("language_id")
                   .IsRequired();

            builder.HasOne(d => d.Category)
                   .WithMany(p => p.CategoryTranslations)
                   .HasForeignKey(d => d.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("category_translation_category_id_fkey");

            builder.HasOne(d => d.Language)
                   .WithMany(p => p.CategoryTranslations)
                   .HasForeignKey(d => d.LanguageId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("category_translation_language_id_fkey");

            builder.HasIndex(e => e.CategoryId)
                   .HasName("category_translation_ix_category_id");

            builder.HasIndex(e => e.LanguageId)
                   .HasName("category_translation_ix_language_id");
        }
    }
}
