using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "dbo");

            builder.HasKey(x => x.Id)
                   .HasName("article_pkey");

            builder.Property(x => x.CreatedDate)
                   .HasColumnName("created_date")
                   .IsRequired();

            builder.Property(e => e.IsActive)
                   .HasColumnName("is_active")
                   .IsRequired();
        }
    }
}
