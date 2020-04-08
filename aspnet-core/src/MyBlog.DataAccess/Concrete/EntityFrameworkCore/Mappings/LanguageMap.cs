using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class LanguageMap : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(e => e.Id)
                   .HasName("language_pkey");

            builder.Property(e => e.IsDefault)
                   .HasColumnName("is_default");

            builder.Property(e => e.LanguageCode)
                .HasColumnName("language_code")
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.HasIndex(e => e.LanguageCode)
                   .HasName("language_code_unique")
                   .IsUnique();
        }
    }
}
