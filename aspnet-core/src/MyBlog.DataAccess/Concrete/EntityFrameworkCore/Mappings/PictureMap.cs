using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class PictureMap : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable("Pictures", "dbo");

            builder.HasKey(e => e.Id)
                   .HasName("picture_pkey");

            builder.Property(e => e.Url)
                   .HasColumnName("url")
                   .IsRequired();

            builder.Property(e => e.IsMain)
                   .HasColumnName("is_main")
                   .IsRequired();

            builder.Property(e => e.PublicId)
                   .HasColumnName("public_id")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(e => e.ArticleId)
                   .HasColumnName("article_id")
                   .IsRequired();
            
            builder.HasOne(d => d.Article)
                   .WithMany(p => p.Pictures)
                   .HasForeignKey(d => d.ArticleId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("picture_article_id_fkey");

            builder.HasIndex(e => e.ArticleId)
                   .HasName("picture_ix_article_id");
        }
    }
}
