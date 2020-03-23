using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");

            builder.HasKey(x => x.Id)
                   .HasName("user_pkey");

            builder.Property(e => e.Email)
                   .HasColumnName("email")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.PasswordHash)
                   .HasColumnName("password_hash")
                   .IsRequired();

            builder.Property(e => e.PasswordSalt)
                   .HasColumnName("password_salt")
                   .IsRequired();

            builder.Property(e => e.Username)
                   .HasColumnName("username")
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
