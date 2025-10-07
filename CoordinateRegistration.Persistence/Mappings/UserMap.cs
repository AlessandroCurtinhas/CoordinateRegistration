using CoordinateRegistration.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoordinateRegistration.Persistence.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Active)
                   .IsRequired()
                   .HasDefaultValue("True");

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.DateCreated)
                   .HasColumnType("SmallDatetime");

            builder.Property(x => x.DateUpdated)
                   .HasColumnType("SmallDatetime");

            builder.Property(x => x.DateDeleted)
                  .HasColumnType("SmallDatetime");

            builder.HasIndex(x => x.Hash)
                   .IsUnique();

            builder.HasIndex(x => x.RecoveryHash)
                   .IsUnique();

            builder.HasIndex(x => x.Email)
                   .IsUnique();
        }
    }
}
