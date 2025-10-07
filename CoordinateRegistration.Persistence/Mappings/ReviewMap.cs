using CoordinateRegistration.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoordinateRegistration.Persistence.Mappings
{
    public class ReviewMap : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Positive)
                    .IsRequired()
                    .HasDefaultValue("False");

            builder.Property(x => x.Negative)
                   .IsRequired()
                   .HasDefaultValue("False");

            builder.HasIndex(x => x.Hash)
                   .IsUnique();

            builder.Property(x => x.DateCreated)
                   .HasColumnType("SmallDatetime");

            builder.Property(x => x.DateUpdated)
                   .HasColumnType("SmallDatetime");
        }
    }
}
